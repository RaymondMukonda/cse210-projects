using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        
        Address address1 = new Address("123 Main St", "New York", "NY", "USA");
        Customer customer1 = new Customer("Alice Johnson", address1);

        Order order1 = new Order(customer1);
        order1.AddProduct(new Product("Laptop", "LAP123", 899.99, 1));
        order1.AddProduct(new Product("Mouse", "MSE456", 25.50, 2));

       
        Address address2 = new Address("456 High St", "London", "LDN", "UK");
        Customer customer2 = new Customer("Bob Smith", address2);

        Order order2 = new Order(customer2);
        order2.AddProduct(new Product("Keyboard", "KEY789", 75.00, 1));
        order2.AddProduct(new Product("Monitor", "MON321", 250.00, 2));
        order2.AddProduct(new Product("USB Cable", "USB654", 5.99, 3));

        
        List<Order> orders = new List<Order> { order1, order2 };

        
        foreach (Order order in orders)
        {
            Console.WriteLine(order.GetPackingLabel());
            Console.WriteLine(order.GetShippingLabel());
            Console.WriteLine($"Total Price: ${order.GetTotalCost():0.00}\n");
            Console.WriteLine(new string('-', 40));
        }
    }
}
