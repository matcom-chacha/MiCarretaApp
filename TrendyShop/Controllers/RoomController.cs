using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrendyShop.Data;
using TrendyShop.Models;
using TrendyShop.ViewModels;

namespace TrendyShop.Controllers
{
    public class RoomController : Controller
    {
        private DataContext dataContext;
        public RoomController(DataContext ctx)
        {
            dataContext = ctx;
        }

        public IActionResult Index()
        {
            var rooms = dataContext.Rooms.ToList();
            List<NamedRoom> vmRoooms = new List<NamedRoom>();
            for (int i = 1; i < 4; i++)
            {
                var name = "Habitación #" + i.ToString();
                vmRoooms.Add(new NamedRoom { RoomId = i, Name = name });
            }
            vmRoooms.Add(new NamedRoom { RoomId = 4, Name = "Almacén" });

            var room1 = dataContext.RoomProducts.Include(r => r.GastronomicProduct).Where(r => r.RoomId == 1).ToList();
            var room2 = dataContext.RoomProducts.Include(r => r.GastronomicProduct).Where(r => r.RoomId == 2).ToList();
            var room3 = dataContext.RoomProducts.Include(r => r.GastronomicProduct).Where(r => r.RoomId == 3).ToList();

            var vm = new RoomViewModel
            {
                Rooms = new List<List<RoomProduct>>
                {
                    room1,
                    room2,
                    room3
                },
                Products = new List<StorageRow>[]
                {
                    dataContext.Storage.Include(p => p.GastronomicProduct).AsEnumerable().Where(p => !room1.Exists(r => r.GastronomicProduct == p.GastronomicProduct) && p.AmountAvailable > 0).ToList(),
                    dataContext.Storage.Include(p => p.GastronomicProduct).AsEnumerable().Where(p => !room2.Exists(r => r.GastronomicProduct == p.GastronomicProduct) && p.AmountAvailable > 0).ToList(),
                    dataContext.Storage.Include(p => p.GastronomicProduct).AsEnumerable().Where(p => !room3.Exists(r => r.GastronomicProduct == p.GastronomicProduct) && p.AmountAvailable > 0).ToList()
                },
                RoomList = vmRoooms
            };
            vm.AmountToAdd1 = new float[vm.Products[0].Count];
            vm.AmountToAdd2 = new float[vm.Products[1].Count];
            vm.AmountToAdd3 = new float[vm.Products[2].Count];

            return View(vm);
        }

        public IActionResult Manage(int roomId)
        {
            var roomProducts = dataContext.RoomProducts.Where(r => r.RoomId == roomId).ToList();
            var storage = dataContext.Storage.ToList();

            var storageToShow = new List<StorageRow>();
            foreach (var storageRow in storage)
            {
                var found = false;
                foreach (var roomProduct in roomProducts)
                {
                    if (roomProduct.Cost == storageRow.Cost && roomProduct.Name == storageRow.Name)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    storageToShow.Add(storageRow);
                }
            }

            var package = dataContext.Package.ToList();
            var amountToAdd = new float[package.Count];
            var amountPossible = new float[package.Count];
            int index = 0;

            foreach (var product in package)
            {
                var _roomProduct = dataContext.RoomProducts.SingleOrDefault(rp => rp.RoomId == roomId
                && rp.Cost == product.Cost && rp.Name == product.Name);

                amountToAdd[index] = (_roomProduct == null) ? product.Quantity : product.Quantity - _roomProduct.Amount;

                var storageRow = dataContext.Storage.Single(sr => sr.Cost == product.Cost && sr.Name == product.Name);

                amountPossible[index] = (storageRow.AmountAvailable - amountToAdd[index] >= 0) ? amountToAdd[index] : storageRow.AmountAvailable;

                index++;
            }

            var viewModel = new ManageRoomViewModel
            {
                RoomId = roomId,
                Storage = storageToShow,
                RoomProducts = roomProducts,
                Package = dataContext.Package.ToList(),
                AmountToAdd = amountToAdd,
                RealAmountToAdd = amountPossible
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Add(RoomViewModel r, int roomId)
        {
            for (int i = 0; i < r.Products[roomId].Count; i++)
            {
                var product = dataContext.GastronomicProducts.Single(p => p.Cost == r.Products[roomId][i].Cost && p.Name == r.Products[roomId][i].Name);
                var room = dataContext.Rooms.Single(r => r.RoomId == (roomId + 1));
                var storageRow = dataContext.Storage.Single(sr => sr.Cost == r.Products[roomId][i].Cost && sr.Name == r.Products[roomId][i].Name);

                if (roomId == 0 && r.AmountToAdd1[i] > 0)
                {
                    dataContext.RoomProducts.Add(new RoomProduct { Cost = r.Products[roomId][i].Cost, Name = r.Products[roomId][i].Name, GastronomicProduct = product, RoomId = roomId, Room = room, Amount = r.AmountToAdd1[i] });
                    storageRow.AmountAvailable -= r.AmountToAdd1[i];
                    storageRow.AmountInRooms += r.AmountToAdd1[i];
                }

                else if (roomId == 1 && r.AmountToAdd2[i] > 0)
                {
                    dataContext.RoomProducts.Add(new RoomProduct { Cost = r.Products[roomId][i].Cost, Name = r.Products[roomId][i].Name, GastronomicProduct = product, RoomId = roomId, Room = room, Amount = r.AmountToAdd2[i] });
                    storageRow.AmountAvailable -= r.AmountToAdd2[i];
                    storageRow.AmountInRooms += r.AmountToAdd2[i];
                }
                else if (roomId == 2 && r.AmountToAdd2[i] > 0)
                {
                    dataContext.RoomProducts.Add(new RoomProduct { Cost = r.Products[roomId][i].Cost, Name = r.Products[roomId][i].Name, GastronomicProduct = product, RoomId = roomId, Room = room, Amount = r.AmountToAdd3[i] });
                    storageRow.AmountAvailable -= r.AmountToAdd3[i];
                    storageRow.AmountInRooms += r.AmountToAdd3[i];
                }
                dataContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateQuantity(float cost, string name, int roomId, int quantity)
        {
            var roomProduct = dataContext.RoomProducts.Single(r => r.RoomId == roomId && r.Cost == cost && r.Name == name);
            var storageRow = dataContext.Storage.Single(sr => sr.Cost == cost && sr.Name == name);

            var difference = quantity - roomProduct.Amount;

            if(storageRow.AmountAvailable < difference)
            {
                //Hay que avisarle al usuario
                return RedirectToAction("Index");
            }
            roomProduct.Amount += difference;
            storageRow.AmountAvailable -= difference;
            storageRow.AmountInRooms += difference;

            dataContext.SaveChanges();

            if (roomProduct.Amount == 0)
            {
                dataContext.RoomProducts.Remove(roomProduct);
            }
            dataContext.SaveChanges();

            return RedirectToAction("Index", "Room");
        }

        //[HttpPost]
        public IActionResult Delete(float cost, string name, int roomId)
        {
            var roomProduct = dataContext.RoomProducts.Single(rp => rp.Cost == cost && rp.Name == name && rp.RoomId == roomId);
            var storageRow = dataContext.Storage.Single(sr => sr.Cost == cost && sr.Name == name);

            storageRow.AmountAvailable += roomProduct.Amount;
            storageRow.AmountInRooms -= roomProduct.Amount;

            dataContext.RoomProducts.Remove(roomProduct);

            dataContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Purchase(int roomId, DateTime _date)
        {
            var products = dataContext.RoomProducts.Include(r => r.GastronomicProduct).Where(rp => rp.RoomId == roomId).ToList();
            var viewModel = new PurchaseViewModel
            {
                RoomProducts = products,
                AmountToMove = new float[products.Count],
                RoomId = roomId,
                InitialDate = _date
            };

            return View(viewModel);
        }

        public IActionResult UpdateStorage(int roomId, DateTime _date)
        {
            var purchase = dataContext.PurchasePerLodgings.Where(pl => pl.RoomId == roomId && pl.Date == _date).ToList();

            foreach (var item in purchase)
            {
                var roomProduct = dataContext.RoomProducts.Single(r => r.RoomId == roomId && r.Cost == item.Cost && r.Name == item.Name);
                var storageRow = dataContext.Storage.Single(sr => sr.Cost == item.Cost && sr.Name == item.Name);

                roomProduct.Amount -= item.Amount;
                storageRow.AmountInRooms -= item.Amount;
                dataContext.SaveChanges();
            }

            return RedirectToAction("Index", "Home");

        }
    }
}