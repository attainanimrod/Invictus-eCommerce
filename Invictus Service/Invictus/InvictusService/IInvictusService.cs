using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace InvictusService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IInvictusService" in both code and config file together.
    [ServiceContract]
    public interface IInvictusService
    {
       
        [OperationContract]
         int Register(string Name, string Surname, string Email, string Password);

        [OperationContract]
         SystemUser getUser(int userId);

        [OperationContract]
        bool UpdateUser(SystemUser user);

        [OperationContract]
        bool DeleteUser(int ID);

        [OperationContract]
        bool UpdateProduct(Product product);
        [OperationContract]
        List<Product> GetAllProducts();

        [OperationContract]
        Product GetProduct(int ID);

        [OperationContract]
        int isAdmin(int ID);

        [OperationContract]
        int Login(string email, string password);

        [OperationContract]
        bool AddProduct(Product product);

        [OperationContract]
        bool DeleteProduct(int id);

        [OperationContract]
        bool addToCart(int prodID, int UserId, int quantity);

        [OperationContract]
        bool removeProductCart(int prodID);

        [OperationContract]
        int getNumberItemsInCart(string userEmail);

        [OperationContract]
        List<Cart> getItemsOnCart(string userEmail);

    }
}
