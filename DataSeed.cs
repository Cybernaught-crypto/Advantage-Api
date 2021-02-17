using System;
using System.Collections.Generic;
using System.Linq;
using Advantage.Api.Models;
using Advantage.API.Models;

namespace Advantage.Api
{
    public class DataSeed
    {
        private readonly ApiContext _ctx;
        public DataSeed(ApiContext ctx)
        {
            _ctx = ctx;
        }

        public void SeedData(int nCustomers, int nOrders)
        {
            // Checking the tables. If no records found then seeding them with data and saving.

            if (!_ctx.Customers.Any())
            {
                SeedCustomers(nCustomers);
                _ctx.SaveChanges();
            }

            if (!_ctx.Orders.Any())
            {
                SeedOrders(nOrders);
                _ctx.SaveChanges();
            }

            if (!_ctx.Servers.Any())
            {
                SeedServers();
                _ctx.SaveChanges();
            }


        }

        private void SeedOrders(int n)
        {
            List<Order> orders = BuildOrderList(n);

            foreach (var order in orders)
            {
                _ctx.Orders.Add(order);
            }
        }

        private void SeedServers()
        {
            List<Server> servers = BuildServerList();

            foreach (var server in servers)
            {
                _ctx.Servers.Add(server);
            }

        }


        private void SeedCustomers(int n)
        {
            List<Customer> customers = BuildCustomerList(n);

            foreach (var customer in customers)
            {
                _ctx.Customers.Add(customer);
            }
        }

        private List<Customer> BuildCustomerList(int nCustomers)
        {

            // Looks at the customer model and generate enteries for each of the properties according to the number of n to create a list.
            var customers = new List<Customer>();
            // making sure we have a unique name
            var names = new List<string>();

            for (var i = 1; i <= nCustomers; i++)
            {
                // methods to create randomised name and values for each of the values.
                var name = Helpers.MakeUniqueCustomerName(names);
                names.Add(name);


                //Add new Customer to the list
                customers.Add(new Customer
                {
                    Id = i,
                    Name = name,
                    Email = Helpers.MakeCustomerEmail(name),
                    State = Helpers.GetRandomState()
                });
            }
            return customers;
        }

        private List<Order> BuildOrderList(int nOrders)
        {
            var orders = new List<Order>();

            var rand = new Random();

            for (var i = 1; i <= nOrders; i++)
            {
                var randCustomerId = rand.Next(1, _ctx.Customers.Count());
                // Outside the Add order because we are using placed to decide if order is completed.
                var placed = Helpers.GetRandomOrderPlaced();
                var completed = Helpers.GetRandomOrderCompleted(placed);
                var customers = _ctx.Customers.ToList();

                orders.Add(new Order
                {
                    Id = i,
                    // a customer must exist to be match with an order.
                    Customer = customers.First(c => c.Id == randCustomerId),
                    Total = Helpers.GetRandomOrderTotal(),
                    Placed = placed,
                    Completed = completed
                });
            }
            return orders;
        }

        private List<Server> BuildServerList()
        {
            return new List<Server>()
            {
                new Server {
                    Id = 1,
                    Name = "Dev-Web",
                    IsOnline = true
                },
                new Server {
                    Id = 2,
                    Name = "Dev-Mail",
                    IsOnline = false
                },
                new Server {
                    Id = 3,
                    Name = "Dev-Services",
                    IsOnline = true
                },
                new Server {
                    Id = 4,
                    Name = "QA-Web",
                    IsOnline = true
                },
                new Server {
                    Id = 5,
                    Name = "QA-Mail",
                    IsOnline = false
                },
                new Server {
                    Id = 6,
                    Name = "QA-Services",
                    IsOnline = true
                },
                new Server {
                    Id = 7,
                    Name = "Prod-Web",
                    IsOnline = true
                },
                new Server {
                    Id = 8,
                    Name = "Prod-Mail",
                    IsOnline = true
                },
                new Server {
                    Id = 9,
                    Name = "Prod-Services",
                    IsOnline = true
                }
            };
        }
    }
}