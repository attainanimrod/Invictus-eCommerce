using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace InvictusService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "InvictusService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select InvictusService.svc or InvictusService.svc.cs at the Solution Explorer and start debugging.
    public class InvictusService : IInvictusService
    {
        InvictusDataClassesDataContext db = new InvictusDataClassesDataContext();
        public bool DeleteUser(int ID)
        {
            var user  = (from p in db.SystemUsers
                         where p.Id == ID
                         select p).FirstOrDefault();
            if(user != null)
            {
                user.Active = 0;
                return true;
            }else
                return false;
        }

        public List<Product> GetAllProducts()
        {
            dynamic Prods = new List<Product>();

            dynamic tempProds = (from p in db.Products
                        where p.Active == 1 select p).DefaultIfEmpty();

            if(tempProds != null)
            {
                foreach (Product p in tempProds)
                {
                    var AllProds = new Product
                    {
                        Prod_ID = p.Prod_ID,
                        Prod_Name = p.Prod_Name,
                        Prod_Image = p.Prod_Image,
                        Prod_Desciption = p.Prod_Desciption,
                        Prod_Price = p.Prod_Price,
                        Prod_Quantity = p.Prod_Quantity,
                        Category = p.Category,
                        Extra_Image1 = p.Extra_Image1,
                        Extra_Image2 = p.Extra_Image2,
                        Extra_Image3 = p.Extra_Image3,
                        DISC_DiscPercent = p.DISC_DiscPercent,
                        DISC_Active = p.DISC_Active,
                        
                    };

                    Prods.Add(AllProds);
                }

                return Prods;
            }
            else
            {
                return null;
            }

           
        }

        public Product GetProduct(int ID)
        {
            var prod = (from p in db.Products
                        where p.Prod_ID == ID
                        select p).FirstOrDefault();

            if (prod != null) 
            {
                var tempProd = prod;

                return tempProd;

        }else
                return null;
        }



        public int Register(string Name, string Surname, string Email, string Password)
        {
            var sysUser = (from s in db.SystemUsers
                           where s.UserEmail.Equals(Email)
                            select s).FirstOrDefault();

            if(sysUser == null)
            {
                var newUser = new SystemUser()
                {
                    UserName = Name,
                    UserEmail = Email,
                    UserPassword = Password,
                    UserSurname = Surname,
                    UserStatus = 1,
                    RegDate = DateTime.Now,
                    UserType = "Customer"
                };

                db.SystemUsers.InsertOnSubmit(newUser);
                try
                {
                    db.SubmitChanges();
                    //success
                    return 1;
                }
                catch (Exception ex)
                {
                    ex.GetBaseException();
                    //error
                    return 0;
                }
            }
            else
            {
                //The user already exist.
                return -1;
            }
        }

        public int Login(string email, string password)
        {
            var user = (from s in db.SystemUsers
                           where s.UserEmail.Equals(email) &&
                           s.UserPassword.Equals(password)
                           select s).SingleOrDefault();

            if (user != null)
            {
                return user.Id;
            }
            else
                return 0;
        }

        public bool UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(SystemUser user)
        {
            var tempUser = (from b in db.SystemUsers where b.Id == user.Id select b).FirstOrDefault();
            tempUser.Id = user.Id;
            tempUser.UserName = user.UserName;
            tempUser.UserSurname = user.UserSurname;
            tempUser.UserEmail = user.UserEmail;
            tempUser.UserPassword = user.UserPassword;

            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                e.GetBaseException();
                return false;
            }
        }

        public SystemUser getUser(int userId)
        {

            var tempUser = (from a in db.SystemUsers where a.Id == userId select a).FirstOrDefault();

            if(tempUser!= null)
            {
                var user = new SystemUser
                {
                    Id = tempUser.Id,
                    UserName = tempUser.UserName,
                    UserEmail = tempUser.UserEmail,
                    UserPassword = tempUser.UserPassword,
                    UserSurname = tempUser.UserSurname,
                    UserStatus = tempUser.UserStatus,
                    UserForgotCode = tempUser.UserForgotCode,
                    UserForgotCodeSentTime = tempUser.UserForgotCodeSentTime

                };

                return user;
            }
            else
            {
                return null;
            }
                
                
        }

        public bool AddProduct(Product product)
        {
            var b = (from a in db.Products where a.Prod_Name.Equals(product.Prod_Name) select a).FirstOrDefault();

            if(b == null)
            {
                var prod = new Product
                { 
                    Prod_Name = product.Prod_Name,
                    Prod_Desciption  = product.Prod_Desciption,
                    Prod_ID = product.Prod_ID,
                    Prod_Image = product.Prod_Image,    
                    Prod_Price = product.Prod_Price,
                    Prod_Quantity = product.Prod_Quantity,
                    DISC_DiscPercent = product.DISC_DiscPercent,
                    Active = product.Active,
                    Category = product.Category,
                    Extra_Image1 = product.Extra_Image1,
                    Extra_Image2 = product.Extra_Image2,
                    Extra_Image3 = product.Extra_Image3,
                    DISC_Active = product.DISC_Active,
                

                };

                db.Products.InsertOnSubmit(prod);

                try
                {
                    db.SubmitChanges();
                    return true;
                }
                catch(Exception e)
                {
                    e.GetBaseException();

                    return false;
                }


            }
            else
            {
                return false;
            }

        }

        public bool DeleteProduct(int id)
        {
            var prod = (from p in db.Products where p.Prod_ID.Equals(id)
                        select p).FirstOrDefault();

            if(prod == null)
            {
                return false;
            }
            else
            {
                prod.Active = 0;

                try
                {
                    db.SubmitChanges();
                    return true;    
                }catch(Exception e)
                {
                    e.GetBaseException();
                    return false;
                }
                
            }
        }

        public int isAdmin(int ID)
        {
            var user = (from a in db.SystemUsers where a.Id == ID select a).FirstOrDefault();

            if(user!= null)
            {
                if (user.UserType == "Admin")
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }

          
        }

        public bool addToCart(int prodID, int UserId, int quantity)
        {
            var prod = (from p in db.Products where p.Prod_ID.Equals(prodID) select p).FirstOrDefault();

            var user = (from u in db.SystemUsers where u.Id.Equals(UserId) select u).FirstOrDefault();

            var cartProd = (from c in db.Carts where c.Prod_ID.Equals(prodID) && c.UserEmail.Equals(user.UserEmail) select c).FirstOrDefault();

            if(cartProd == null)
            {
                //adding new product
                var cartitem = new Cart
                {
                    UserEmail = user.UserEmail,
                    Prod_ID = prod.Prod_ID,
                    Prod_Name = prod.Prod_Name,
                    Prod_Image = prod.Prod_Image,
                    Prod_Price = prod.Prod_Price,
                    Quantity = quantity

                };

                db.Carts.InsertOnSubmit(cartitem);

                try
                {
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception e)
                {
                    e.GetBaseException();

                    return false;
                }
            }
            else
            {
                //Adding quantity of product
                

                try
                {
                    cartProd.Quantity += quantity;

                    db.SubmitChanges();

                    return true;
                }
                catch(Exception e)
                {
                    e.GetBaseException();

                    return false;
                }
            }
            

        }


        public bool removeProductCart(int prodID)
        {
            var cartItem = (from c in db.Carts where c.Prod_ID.Equals(prodID) select c).FirstOrDefault();

            if( cartItem != null )
            {
                if(cartItem.Quantity > 1)
                {
                   //reducing pruduct quantity
                   try
                    {
                        cartItem.Quantity -= 1;

                        db.SubmitChanges();

                        return true;
                    }
                    catch(Exception e)
                    {
                        e.GetBaseException();
                        return false;
                    }
                }
                else
                {
                    //deleting the while cart item
                    try
                    {
                        db.Carts.DeleteOnSubmit(cartItem);

                        return true;
                    }
                    catch(Exception e)
                    {
                        e.GetBaseException();

                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }


       public List<Cart> getItemsOnCart(string userEmail)
        {
            List<Cart> cartItems = new List<Cart>();

            dynamic tempItems = (from c in db.Carts where c.UserEmail.Equals(userEmail) select c).DefaultIfEmpty();

            if(tempItems !=null)
            {
                foreach(Cart item in tempItems)
                {
                    var temItem = new Cart
                    {
                        Cart_ID = item.Cart_ID,
                        UserEmail = userEmail,
                        Prod_ID = item.Prod_ID,
                        Prod_Name = item.Prod_Name,
                        Prod_Price = item.Prod_Price,
                        Prod_Image = item.Prod_Image,
                        Quantity = item.Quantity
                    };

                    cartItems.Add(temItem);
                }

                return cartItems;
            }
            else
            {
                return null;
            }
        }



        public int getNumberItemsInCart(string userEmail)
        {
            //getting list of cart items with the user's email
            dynamic cartItems = (from cart in db.Carts where cart.UserEmail.Equals(userEmail) select cart).DefaultIfEmpty();

            int numberItems = 0;

            //checking if cartItems is not empty
            if(cartItems != null)
            {
                foreach(Cart item in cartItems)
                {
                    numberItems +=1;
                }
            }
            
            //returning number of items
            return numberItems;
        }
    }
}
