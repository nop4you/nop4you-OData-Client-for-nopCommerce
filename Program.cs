using Default;
using System;

namespace ODataClientNopCommerce
{
    public class Program
    {
        public static string UserName = "admin@yourStore.com";
        public static string Password = "1234567";
        public static string StoreUrl = "yourStore";

        static int Main(string[] args)
        {
            Console.WriteLine("Start application.");

            Token token = new Token(StoreUrl, UserName, Password);
            ODataServices.InitContainer(token.ApiToken, StoreUrl);

            /* CRUD - Categories
            ******************************************************** 
            //Get all categories
            var categories = ODataServices.GetCategories();

            //Insert new category
            var category = new Category();
            category.Name = "Test 1" + DateTime.UtcNow.ToString();
            category = ODataServices.InsertCategory(category);

            //Get category by id
            var samplecategory = category = ODataServices.GetCategoryById(category.Id);

            //Update category
            category.Name = "Test 2" + DateTime.UtcNow.ToString(); 
            category = ODataServices.UpdateCategory(category);

            //Delete category
            ODataServices.DeleteCategory(category);

            ******************************************************** 
            */

            /*Assign customer to role*/
            var customer = ODataServices.GetCustomerByEmail("admin@yourStore.com");
            var role = ODataServices.GetFirstCustomerRole();
            ODataServices.AssignCustomerRoleToCustomer(role, customer);

            Console.ReadKey();
            return 0;
        }
    }
}
