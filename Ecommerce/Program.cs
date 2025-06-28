// using System;
// using myexceptions;
// using util;
// class Program
// {
//     static void Main()
//     {
//         try
//         {
//             var conn = util.DBConnection.GetConnection();
//             Console.WriteLine("Connected to database.");

//             int customerId = 999; 
//             bool customerExists = false;

//             if (!customerExists)
//                 throw new CustomerNotFoundException("Customer ID not found: " + customerId);
//         }
//         catch (CustomerNotFoundException ex)
//         {
//             Console.WriteLine("❌ " + ex.Message);
//         }
//         catch (ProductNotFoundException ex)
//         {
//             Console.WriteLine("❌ " + ex.Message);
//         }
//         catch (OrderNotFoundException ex)
//         {
//             Console.WriteLine("❌ " + ex.Message);
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine("General Error: " + ex.Message);
//         }
//     }
// }
