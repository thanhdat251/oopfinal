using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using NPL.SMS.R2S.Training.Entities;
using NPL.SMS.R2S.Training.DAO;
using NPL.SMS.Connection;

namespace NPL.SMS.R2S.Training.Main
{
    class SaleManagement
    {
        static int Menu()
        {
            int option;

            Console.WriteLine("\t\t\t\t\t***");
            Console.WriteLine("\t ________________________________________________________________________");
            Console.WriteLine("\t|                              FUNCTION TABLE:                          |");
            Console.WriteLine("\t|_______________________________________________________________________|");
            Console.WriteLine("\t| Function 1: |List with all customers into the order table             |");
            Console.WriteLine("\t|_____________|_________________________________________________________|");
            Console.WriteLine("\t| Function 2: |List with all the orders for a given customer id         |");
            Console.WriteLine("\t|_____________|_________________________________________________________|");
            Console.WriteLine("\t| Function 3: |List all lineItem for an order                           |");
            Console.WriteLine("\t|_____________|_________________________________________________________|");
            Console.WriteLine("\t| Function 4: |Compute Order Total                                      |");
            Console.WriteLine("\t|_____________|_________________________________________________________|");
            Console.WriteLine("\t| Function 5: |Add a Customer into the database                         |");
            Console.WriteLine("\t|_____________|_________________________________________________________|");
            Console.WriteLine("\t| Function 6: |Delete a Customer                                        |");
            Console.WriteLine("\t|_____________|_________________________________________________________|");
            Console.WriteLine("\t| Function 7: |Update a customer in the database                        |");
            Console.WriteLine("\t|_____________|_________________________________________________________|");
            Console.WriteLine("\t| Function 8: |Create an order into the database                        |");
            Console.WriteLine("\t|_____________|_________________________________________________________|");
            Console.WriteLine("\t| Function 9: |Creat a lineitem into the database                       |");
            Console.WriteLine("\t|_____________|_________________________________________________________|");
            Console.WriteLine("\t| Function 10:|                                                         |");
            Console.WriteLine("\t|_____________|_________________________________________________________|");
            Console.WriteLine("\t| Function 11: | Exit                                                   |");
            Console.WriteLine("\t|_____________|_________________________________________________________|");
            Console.WriteLine("\t\t\t\t\t***\n");
            Console.Write("Please select function mumber: ");
            do
            {
                option = int.Parse(Console.ReadLine());

                if (option < 1 || option > 11)
                    Console.Write("This function is invalid! Please select function mumber: ");
            } while (option < 1 || option > 11);

            return option;
        }

        static void Main(string[] args)
        {
            // Nhập xuất dữ liệu tiếng việt
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            LineItemDAO LN = new LineItemDAO();
            CustomerDAO CD = new CustomerDAO();
            OrderDAO OD = new OrderDAO();

            int option;

            do
            {
                option = Menu();
                switch (option)
                {
                    case 1:
                        {
                            Console.WriteLine("\n---------------");
                            List<Customer> list = CD.GetAllCustomer();
                            try
                            {
                                foreach (Customer t in list)
                                {
                                    Console.WriteLine($"|      {t.CustomerId}       |     {t.CustomerName}");
                                }
                            }
                            catch
                            {
                                Console.WriteLine("Rỗng");
                            }
                            Console.WriteLine("|______________|______________________|");
                        }
                        break;
                    case 2:
                        {
                            Console.Write("Please enter customer id: ");
                            int Customer_Id = int.Parse(Console.ReadLine());
                            List<Order> listOrderByID = OD.GetAllOrdersById(Customer_Id);
                            Console.WriteLine("=================================CHỨC NĂNG 2=========================================");
                            Console.WriteLine(" _____________________________________________________________________________________________________");
                            Console.WriteLine("|     Order Id    |           OrderDate             |   Customer ID    |   EmployeeID    |   Total   |");
                            Console.WriteLine("|_________________|_________________________________|__________________|_________________|___________|");
                            foreach (Order m in listOrderByID)
                            {
                                Console.WriteLine($"|      {m.OrderId}          |      {m.OrderDate}        |        {m.CustomerID}         |        {m.EmployeeID}       |    {m.Total}      |");
                            }
                            Console.WriteLine("|_________________|_________________________________|__________________|_________________|___________|");
                        }
                        break;
                    case 3:
                        {
                            Console.WriteLine("=================================CHỨC NĂNG 3=========================================");
                            Console.Write("Enter Orderid : ");
                            int id = int.Parse(Console.ReadLine());
                            List<LineItem> listLineItemByOrderID = LN.GetAllItemsByOderId(id);
                            Console.WriteLine(" ________________________________________________________________________");
                            Console.WriteLine("| OrderId Id   | ProductID             |Quantity         |Price           ");
                            Console.WriteLine("|______________|_______________________|_________________|_______________ ");

                            try
                            {
                                foreach (LineItem t in listLineItemByOrderID)
                                {
                                    Console.WriteLine($"|     {t.OrderID}        |       {t.ProdcutId}               |       {t.Quantity}         |       {t.Price}          |");
                                }
                            }
                            catch
                            {
                                Console.WriteLine("Rỗng");
                            }
                            Console.WriteLine("|______________|_______________________|_________________|______________________|");
                        }
                        break;
                    case 4:
                        {
                            Console.WriteLine("=================================CHỨC NĂNG 4=========================================");
                            Console.Write("Enter Orderid : ");
                            int orderId = int.Parse(Console.ReadLine());

                            Console.WriteLine(" ________________________________________________________________________");
                            Console.WriteLine("|                           total                                        ");
                            Console.WriteLine("|_______________________________________________________________________ ");

                            try
                            {


                                Console.WriteLine($"|                            {OD.ComputeOrderTotal(orderId)}                         ");

                            }
                            catch
                            {
                                Console.WriteLine("Rỗng");
                            }
                            Console.WriteLine("|_______________________________________________________________________|");
                        }
                        break;

                    case 5:
                        {
                            Customer customer = new Customer();
                            Console.WriteLine("Enter CustomerName: ");
                            customer.customerName = Console.ReadLine();

                            CD.AddCustomer(customer);

                            Console.WriteLine("Add Success");
                        }
                        break;
                    case 6:
                        {
                            Console.WriteLine("Enter CustomerID want to delete: ");

                            int customerID = int.Parse(Console.ReadLine());

                            CD.DeleteCustomer(customerID);
                        }
                        break;
                    case 7:
                        {
                            Customer customer = new Customer();

                            Console.Write("Enter customer id: ");
                            customer.CustomerId = int.Parse(Console.ReadLine());

                            Console.Write("Enter customer name: ");
                            customer.CustomerName = Console.ReadLine();

                            try
                            {
                                CD.UpdateCustomer(customer);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        break;
                    case 8:
                        {
                            Order order = new Order();

                            order.OrderDate = DateTime.Now;

                            Console.Write("Enter customer id: ");
                            order.CustomerID = int.Parse(Console.ReadLine());

                            Console.Write("Enter employee id: ");
                            order.EmployeeID = int.Parse(Console.ReadLine());

                            order.Total = 0;

                            try
                            {
                                OD.AddOrder(order);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        break;
                    case 9:
                        {
                            LineItem lineItem = new LineItem();

                            Console.WriteLine("Enter OrderId: ");
                            lineItem.OrderID = int.Parse(Console.ReadLine());

                            Console.WriteLine("Enter ProductId: ");
                            lineItem.ProdcutId = int.Parse(Console.ReadLine());

                            Console.WriteLine("Enter Quantity: ");
                            lineItem.Quantity = int.Parse(Console.ReadLine());

                            LN.AddLineItem(lineItem);
                            OD.UpdateOrderTotal(lineItem.OrderID);
                        }
                        break;
                    case 10:
                        break;
                    default:
                        break;
                }
            } while (option != 11);
        }
    }
}
