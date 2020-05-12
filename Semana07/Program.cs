using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semana07
{
    class Program
    {

        public static DataClasses1DataContext context = new DataClasses1DataContext();
        static void Main(string[] args)
        {
            Joining();
            Console.Read();
        }

        static void IntroToLINQ()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            var numQuery =
                from num in numbers
                where (num % 2) == 0
                select num;

            foreach (int num in numQuery)
            {
                Console.Write("{0,1}", num);
            }

            Console.Write("----LAMBVERSION----");

            var numQueryLamb = numbers.Where(c => c % 2 == 0).Select(c => c);

            foreach (int num in numQueryLamb)
            {
                Console.Write("{0,1}", num);
            }

        }

        static void DataSource()
        {
            var queryAllCustomers = from cust in context.clientes
                                    select cust;

            foreach (var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }

            Console.Write("----LAMBVERSION----");

            var queryAllCustomersLamb = context.clientes.Select(c => c);

            foreach (var item in queryAllCustomersLamb)
            {
                Console.WriteLine(item.NombreCompañia);
            }

        }

        static void Filtering()
        {
            var queryLondonCustomers = from cust in context.clientes
                                       where cust.Ciudad == "Londres"
                                       select cust;

            foreach (var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }

            Console.Write("----LAMBVERSION----");

            var queryLondonCustomersLamb = context.clientes.Where(c => c.Ciudad == "Londres").Select(c => c);

            foreach (var item in queryLondonCustomersLamb)
            {
                Console.WriteLine(item.Ciudad);
            }
        }

        static void Ordering()
        {
            var queryLondonCustomers3 =
                            from cust in context.clientes
                            where cust.Ciudad == "Londres"
                            orderby cust.NombreCompañia ascending
                            select cust;

            foreach( var item in queryLondonCustomers3)
            {
                Console.WriteLine(item.NombreCompañia);
            }

            Console.Write("----LAMBVERSION----");

            var queryLondonCustomers3Lamb = context.clientes.Where(c => c.Ciudad == "Londres").OrderBy(c => c.NombreCompañia).Select(c => c);

            foreach (var item in queryLondonCustomers3Lamb)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        static void Grouping()
        {
            var queryCustomersByCity =
                            from cust in context.clientes
                            group cust by cust.Ciudad;

            foreach (var customersGroup in queryCustomersByCity)
            {
                Console.WriteLine(customersGroup.Key);
                foreach (clientes customer in customersGroup)
                {
                    Console.WriteLine("         {0}", customer.NombreCompañia);
                }
            }

            Console.Write("----LAMBVERSION----");

            var queryCustomersByCityLamb = context.clientes.GroupBy(c => c.Ciudad);

            foreach (var customersGroup in queryCustomersByCityLamb)
            {
                Console.WriteLine(customersGroup.Key);
                foreach (clientes customer in customersGroup)
                {
                    Console.WriteLine("         {0}", customer.NombreCompañia);
                }
            }
        }

        static void Grouping2()
        {
            var custQuery =
                    from cust in context.clientes
                    group cust by cust.Ciudad into custGroup
                    where custGroup.Count() > 2
                    orderby custGroup.Key
                    select custGroup;

            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }

            Console.Write("----LAMBVERSION----");

            var custQueryLamb = context.clientes.GroupBy(c => c.Ciudad).Where( c=> c.Count()>2).OrderBy(c => c.Key).Select(c => c);

            foreach (var item in custQueryLamb)
            {
                Console.WriteLine(item.Key);
            }
        }

        static void Joining()
        {
            var innerJoinQuery =
                    from cust in context.clientes
                    join dist in context.Pedidos on cust.idCliente equals dist.IdCliente
                    select new { CustomerName = cust.NombreCompañia, DistributorName = dist.PaisDestinatario };

            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }

            Console.Write("----LAMBVERSION----------------------------------------");

            var innerJoinQueryLamb = context.clientes.Join(context.Pedidos,
                cli => cli.idCliente, ped => ped.IdPedido, (cli, ped) => new { cli.NombreCompañia, ped.PaisDestinatario });

            foreach (var item in innerJoinQueryLamb)
            {
                Console.WriteLine(item.CustomerName);
            }
        }
    }
}
