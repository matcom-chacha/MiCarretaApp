using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrendyShop.Data;
using TrendyShop.Models;
using TrendyShop.ViewModels;

namespace TrendyShop.Controllers
{
    [Authorize]
    public class StorageController : Controller
    {
        private DataContext dataContext;
        private UserManager<IdentityUser> userManager;
        public StorageController(DataContext ctx, UserManager<IdentityUser> _userManager)
        {
            dataContext = ctx;
            userManager = _userManager;
        }

        public IActionResult Index()
        {
            //var productsAtHome = dataContext.Storage.Where(p => p.AmountAvailable > 0 || p.AmountInRooms > 0);
            var gastronomicProducts = dataContext.Storage.Include(s => s.GastronomicProduct).ToList();
            var expProducts = dataContext.ExpendableProductStorage.ToList();

            var rooms = new List<NamedRoom>();//ahora es 4 o 0, pero luego puede asignarsele una numero especial

            for (int i = 1; i < 4; i++)
            {
                var name = "Habitación #" + i.ToString();
                rooms.Add(new NamedRoom { RoomId = i, Name = name });
            }
            rooms.Add(new NamedRoom { RoomId = 4, Name = "Almacén" });


            var vm = new StorageIndexviewModel
            {
                ExpProducts = expProducts,
                GastronomicProducts = gastronomicProducts,
                Rooms = rooms,
            };
            return View(vm);
        }

        public IActionResult AddProduct()
        {
            var newProductViewModel = new NewProductViewModel
            {
                GastronomicProduct = new GastronomicProduct()
            };
            return View(newProductViewModel);
        }
        [HttpPost]
        public IActionResult InsertNewProduct(NewProductViewModel npvm)
        {
            if (!ModelState.IsValid)
            {
                return View("AddProduct", npvm);
            }

            dataContext.GastronomicProducts.Add(npvm.GastronomicProduct);

            var initialDeposit = new GastronomicProductOperation
            {
                Cost = npvm.GastronomicProduct.Cost,
                Name = npvm.GastronomicProduct.Name,
                GastronomicProduct = npvm.GastronomicProduct,
                Date = DateTime.Now,
                AmountAvailable = 0,
                AmountInrooms = 0,
                OperationAmount = npvm.InitialAmount,
                POType = dataContext.ProductOperationTypes.Single(p => p.Name == "Deposit")
            };

            dataContext.GastronomicProductOperations.Add(initialDeposit);
            dataContext.SaveChanges();

            var storageRow = new StorageRow
            {
                Cost = npvm.GastronomicProduct.Cost,
                Name = npvm.GastronomicProduct.Name,
                GastronomicProduct = npvm.GastronomicProduct,
                AmountAvailable = npvm.InitialAmount,
                AmountInRooms = 0
            };
            dataContext.Storage.Add(storageRow);

            dataContext.SaveChanges();
            return RedirectToAction("Index", "Storage");
        }


        //public IActionResult Refill()
        //{
        //    var rpvm = new RefillProductViewModel
        //    {
        //        StorageRow = new StorageRow(),
        //        GastronomicProducts = dataContext.GastronomicProducts.ToList(),
        //        ExpendableProducts = dataContext.ExpendableProducts.ToList()
        //    };
        //    return View(rpvm);
        //}

        //[HttpPost]
        //public IActionResult InsertProductRefill()
        //{
        //    return RedirectToAction("Index", "Storage");
        //}
        public IActionResult RefillExpProd(float productCost, string productName)
        {
            var vm = new EditExpProdStorageViewModel
            {
                ExpProduct = dataContext.ExpendableProductStorage.Single(sr => sr.Cost == productCost && sr.Name == productName),
            };

            return View(vm);
        }
        public IActionResult UpdateRefillExpProd(EditExpProdStorageViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Refill", vm);
            }

            var expProduct = dataContext.ExpendableProductStorage.Single(sr => sr.Cost == vm.ExpProduct.Cost && sr.Name == vm.ExpProduct.Name);
            var opType = dataContext.ProductOperationTypes.Single(p => p.Name == "Deposit");
            var employeeId = userManager.GetUserId(User);

            var replenishInfo = new ExpendableProductOperation
            {
                Cost = expProduct.Cost,
                Name = expProduct.Name,
                Date = DateTime.Now,
                AlreadyInStorageAmount = (float)expProduct.TotalQuantity,
                OperationAmount = vm.AmountToAdd,
                POType = opType,
                POTypeId = opType.POTypeId,
                EmployeeId = employeeId,
                Employee = dataContext.Employees.Find(employeeId)
            };
            dataContext.ExpendableProductOperations.Add(replenishInfo);
            dataContext.SaveChanges();

            expProduct.TotalQuantity += vm.AmountToAdd;
            expProduct.LastInsertedQuantity = vm.AmountToAdd;
            expProduct.LastInsertionDate = DateTime.Now;
            dataContext.SaveChanges();

            return RedirectToAction("ExpProdDetails", "Storage", new { productCost = vm.ExpProduct.Cost, productName = vm.ExpProduct.Name});
        }

        public IActionResult Refill(float productCost, string productName)
        {
            var esrvm = new EditStorageRowViewModel
            {
                StorageRow = dataContext.Storage.Include(sr => sr.GastronomicProduct).Single(sr => sr.Cost == productCost && sr.Name == productName),
                Cost = productCost,
                Name = productName
            };

            return View(esrvm);
        }
        public IActionResult UpdateRefill(float cost, string name, int amountToAdd)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var storageRowToUpdate = dataContext.Storage.Single(sr => sr.Cost == cost && sr.Name == name);
            var employeeId = userManager.GetUserId(User);

            var deposit = new GastronomicProductOperation
            {
                Cost = cost,
                Name = name,
                GastronomicProduct = storageRowToUpdate.GastronomicProduct,
                Date = DateTime.Now,
                AmountAvailable = storageRowToUpdate.AmountAvailable,
                AmountInrooms = storageRowToUpdate.AmountInRooms,
                OperationAmount = amountToAdd,
                POType = dataContext.ProductOperationTypes.Single(p => p.Name == "Deposit"),
                EmployeeId = employeeId,
                Employee = dataContext.Employees.Find(employeeId)
            };
            dataContext.GastronomicProductOperations.Add(deposit);
            dataContext.SaveChanges();

            storageRowToUpdate.AmountAvailable += amountToAdd;
            dataContext.SaveChanges();

            return RedirectToAction("GastProdDetails", "Storage", new { productCost = cost, productName = name }) ;
        }

        public IActionResult StorageRow(StorageRow storage)
        {
            return View(storage);
        }

        public IActionResult AddExpProduct()
        {
            var vm = new AddExpProductViewModel
            {
                ExpendableProduct = new ExpendableProduct()
            };
            return View(vm);
        }
        public IActionResult SaveExpProduct(AddExpProductViewModel vm)
        {
            dataContext.ExpendableProducts.Add(vm.ExpendableProduct);
            var opType = dataContext.ProductOperationTypes.Single(p => p.Name == "Deposit");

            var replenishInfo = new ExpendableProductOperation
            {
                Cost = vm.ExpendableProduct.Cost,
                Name = vm.ExpendableProduct.Name,
                Date = DateTime.Now,
                AlreadyInStorageAmount = 0,
                OperationAmount = vm.Quantity,
                POType = opType,
                POTypeId = opType.POTypeId
            };
            dataContext.ExpendableProductOperations.Add(replenishInfo);
            dataContext.SaveChanges();

            dataContext.ExpendableProductStorage.Add(
                new ExpendableProductStorage
                {
                    Cost = vm.ExpendableProduct.Cost,
                    Name = vm.ExpendableProduct.Name,
                    ExpendableProduct = vm.ExpendableProduct,
                    LastInsertedQuantity = vm.Quantity,
                    TotalQuantity = vm.Quantity,
                    LastInsertionDate = DateTime.Now
                }
            );
            dataContext.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public IActionResult GastProdDetails(float productCost, string productName)
        {
            var vm = new GastronomicProductDetailsViewModel
            {
                Deposits = dataContext.GastronomicProductOperations.Include(p => p.Employee).Where(p => p.Cost == productCost && p.Name == productName && p.POType.Name == "Deposit").ToList(),
                Extractions = dataContext.GastronomicProductOperations.Include(p => p.Employee).Where(p => p.Cost == productCost && p.Name == productName && p.POType.Name == "Extraction").ToList(),
                ProductName = productName
            };

            return View(vm);
        }
        public IActionResult ExpProdDetails(float productCost, string productName)
        {
            var vm = new ExpendableProductDetailsViewModel
            {
                Deposits = dataContext.ExpendableProductOperations.Include(p => p.Employee).Where(p => p.Cost == productCost && p.Name == productName && p.POType.Name == "Deposit").ToList(),
                Extractions = dataContext.ExpendableProductOperations.Include(p => p.Employee).Where(p => p.Cost == productCost && p.Name == productName && p.POType.Name == "Extraction").ToList(),
                ProductName = productName
            };
            return View(vm);
        }
        
        public IActionResult Replace(int sourceId, int destinationId)
        {
            if (destinationId == sourceId || sourceId == 0 || destinationId == 0)
            {
                return RedirectToAction("Index", "Lodging");
            }

            var products = new List<GastronomicProduct>();
            var InSource = new List<float>();

            if (sourceId == 4)
            {
                var storage = dataContext.Storage.Where(sr => sr.AmountAvailable > 0).Include(sr => sr.GastronomicProduct);
                foreach (var sr in storage)
                {
                    products.Add(sr.GastronomicProduct);
                    InSource.Add(sr.AmountAvailable);
                }
            }
            else
            {
                var source = dataContext.RoomProducts.Where(r => r.RoomId == sourceId).Include(r => r.GastronomicProduct);
                foreach (var rp in source)
                {
                    products.Add(rp.GastronomicProduct);
                    InSource.Add(rp.Amount);
                }
            }

            var InDestination = new float[products.Count];

            if (destinationId == 4)
            {
                for (int i = 0; i < products.Count(); i++)
                {
                    var productsInDestination = dataContext.Storage.SingleOrDefault(sr => sr.Name == products[i].Name && sr.Cost == products[i].Cost);
                    InDestination[i] = productsInDestination == null ? 0 : productsInDestination.AmountAvailable;
                }
            }
            else
            {
                for (int i = 0; i < products.Count(); i++)
                {
                    var productsInDestination = dataContext.RoomProducts.SingleOrDefault(rp => rp.Name == products[i].Name && rp.Cost == products[i].Cost && rp.RoomId == destinationId);
                    InDestination[i] = productsInDestination == null ? 0 : productsInDestination.Amount;
                }
            }

            var sourceName = sourceId == 4 ? "Almacén" : "Hab " + sourceId.ToString();
            var destinationName = destinationId == 4 ? "Almacén" : "Hab " + destinationId.ToString();

            var rvm = new ReplaceViewModel
            {
                Source = InSource,
                Destination = InDestination,
                SourceId = sourceId,
                SourceName = sourceName,
                DestinationName = destinationName,
                DestinationId = destinationId,
                AmountToMove = new float[products.Count],
                ProductsInSource = products
            };
            return View(rvm);
        }
        [HttpPost]
        public IActionResult SaveReplace(ReplaceViewModel rvm)
        {
            //if (Validar)

            for (int i = 0; i < rvm.AmountToMove.Length; i++)
            {
                if (rvm.AmountToMove[i] == 0)
                    continue;

                if (rvm.SourceId == 4) //comprobar que exista esa cantidad
                {
                    var storageRow = dataContext.Storage.SingleOrDefault(sr => sr.Name == rvm.ProductsInSource[i].Name && sr.Cost == rvm.ProductsInSource[i].Cost);
                    //if(storageRow is null) Error

                    storageRow.AmountAvailable -= rvm.AmountToMove[i];
                    dataContext.SaveChanges();
                }
                else
                {
                    var roomProduct = dataContext.RoomProducts.SingleOrDefault(sr => sr.RoomId == rvm.SourceId && sr.Name == rvm.ProductsInSource[i].Name && sr.Cost == rvm.ProductsInSource[i].Cost);
                    //if(storageRow is null) Error


                    roomProduct.Amount -= rvm.AmountToMove[i];
                    dataContext.SaveChanges();

                    if (roomProduct.Amount == 0)
                        dataContext.RoomProducts.Remove(roomProduct);


                    var storageRow = dataContext.Storage.SingleOrDefault(sr => sr.Name == rvm.ProductsInSource[i].Name && sr.Cost == rvm.ProductsInSource[i].Cost);
                    storageRow.AmountInRooms -= rvm.AmountToMove[i];
                    dataContext.SaveChanges();

                }

                if (rvm.DestinationId == 4)
                {
                    var storageRow = dataContext.Storage.SingleOrDefault(sr => sr.Name == rvm.ProductsInSource[i].Name && sr.Cost == rvm.ProductsInSource[i].Cost);
                    //if(storageRow is null) Error

                    storageRow.AmountAvailable += rvm.AmountToMove[i];
                    dataContext.SaveChanges();
                }
                else
                {
                    var roomProduct = dataContext.RoomProducts.SingleOrDefault(sr => sr.RoomId == rvm.DestinationId && sr.Name == rvm.ProductsInSource[i].Name && sr.Cost == rvm.ProductsInSource[i].Cost);
                    //if(storageRow is null) Error

                    if (roomProduct == null)
                    {
                        roomProduct = new RoomProduct
                        {
                            Name = rvm.ProductsInSource[i].Name,
                            Cost = rvm.ProductsInSource[i].Cost,
                            RoomId = rvm.DestinationId,
                            Amount = rvm.AmountToMove[i]
                        };
                        dataContext.RoomProducts.Add(roomProduct);
                    }
                    else
                    {
                        roomProduct.Amount += rvm.AmountToMove[i];

                    }
                    dataContext.SaveChanges();

                    var storageRow = dataContext.Storage.SingleOrDefault(sr => sr.Name == rvm.ProductsInSource[i].Name && sr.Cost == rvm.ProductsInSource[i].Cost);
                    storageRow.AmountInRooms += rvm.AmountToMove[i];
                    dataContext.SaveChanges();
                }
            }
            return RedirectToAction("Replace", "Storage", new { sourceId = rvm.SourceId, destinationId = rvm.DestinationId });

        }

        public IActionResult ExtractGastronomicProduct(float productCost, string productName)
        {
            var vm = new ExtractGProductViewModel
            {
                ProductInfo = dataContext.Storage.Include(p => p.GastronomicProduct).Single(p => p.Cost == productCost && p.Name == productName),
                ProductCost = productCost,
                ProductName = productName
            };
            return View(vm);
        }
        public IActionResult SaveExtractGastronomicProduct(float productCost, string productName, float amountToExtract)
        {
            var gProduct = dataContext.Storage.Find(productName, productCost);
            var opType = dataContext.ProductOperationTypes.Single(p => p.Name == "Extraction");
            var employeeId = userManager.GetUserId(User);
            
            var extraction = new GastronomicProductOperation
            {
                Cost = productCost,
                Name = productName,
                GastronomicProduct = dataContext.GastronomicProducts.Find(productCost, productName),
                AmountAvailable = gProduct.AmountAvailable,
                AmountInrooms = gProduct.AmountInRooms,
                POType = opType,
                POTypeId = opType.POTypeId,
                Date = DateTime.Now,
                OperationAmount = amountToExtract,
                EmployeeId = employeeId,
                Employee = dataContext.Employees.Find(employeeId)
            };
            gProduct.AmountAvailable -= amountToExtract;

            dataContext.GastronomicProductOperations.Add(extraction);
            dataContext.SaveChanges();

            return RedirectToAction("GastProdDetails", "Storage", new { productCost = productCost, productName = productName });

        }

        public IActionResult ExtractExpendableProduct(float productCost, string productName)
        {
            var vm = new ExtractEProductViewModel
            {
                ProductInfo = dataContext.ExpendableProductStorage.Include(p => p.ExpendableProduct).Single(p => p.Cost == productCost && p.Name == productName),
                ProductCost = productCost,
                ProductName = productName
            };
            return View(vm);
        }
        public IActionResult SaveExtractExpendableProduct(float productCost, string productName, float amountToExtract)
        {
            var eProduct = dataContext.ExpendableProductStorage.Find(productCost, productName);
            var opType = dataContext.ProductOperationTypes.Single(p => p.Name == "Extraction");
            var employeeId = userManager.GetUserId(User);

            var extraction = new ExpendableProductOperation
            {
                Cost = productCost,
                Name = productName,
                ExpendableProduct = dataContext.ExpendableProducts.Find(productCost, productName),
                AlreadyInStorageAmount = (float)eProduct.TotalQuantity,
                POType = opType,
                POTypeId = opType.POTypeId,
                Date = DateTime.Now,
                OperationAmount = amountToExtract,
                EmployeeId = employeeId,
                Employee = dataContext.Employees.Find(employeeId)
            };
            eProduct.TotalQuantity -= amountToExtract;

            dataContext.ExpendableProductOperations.Add(extraction);
            dataContext.SaveChanges();

            return RedirectToAction("ExpProdDetails", "Storage", new { productCost = productCost, productName = productName });

        }
    }
}