using Default;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ODataClientNopCommerce
{
    public class ODataServices
    {
        private static string Token { get; set; }
        private static string StoreUrl { get; set; }

        private static Container container;

        public static void onBuildingRequest(object sender, Microsoft.OData.Client.BuildingRequestEventArgs e)
        {
            e.Headers.Add("Authorization", "Bearer " + Token);
        }

        public static void InitContainer(string token, string storeurl)
        {
            Token = token;
            StoreUrl = storeurl;
            container = new Container(new Uri(StoreUrl + "/odata/"));
            container.BuildingRequest += onBuildingRequest;
        }
        #region Category

        public static List<Category> GetCategories()
        {
            return container.Category.ToList();
        }

        public static Category GetCategoryById(int id)
        {
            return container.Category.Where(x => x.Id == id).FirstOrDefault();
        }

        public static Category InsertCategory(Category category)
        {
            container.AddToCategory(category);
            container.SaveChanges();
            return category;
        }

        public static Category UpdateCategory(Category category)
        {
            container.UpdateObject(category);
            container.SaveChanges();
            return category;
        }

        public static bool DeleteCategory(Category category)
        {
            container.DeleteObject(category);
            container.SaveChanges();
            return true;
        }

        #endregion

        #region Customer

        public static Customer GetCustomerByEmail(string email)
        {
            return container.Customer.Where(x => x.Email == email).FirstOrDefault();
        }

        public static CustomerRole GetFirstCustomerRole()
        {
            return container.CustomerRole.Where(x=>x.SystemName == "ForumModerators").FirstOrDefault();
        }

        public static void AssignCustomerRoleToCustomer(CustomerRole role, Customer customer)
        {
            //customer.CustomerRoles.Add(role);
            container.AddLink(customer, "CustomerRoles", role);
            container.SaveChanges();
        }

        #endregion

    }
}
