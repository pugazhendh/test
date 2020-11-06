using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using System.Web;
using System.Web.Http;
using Oshopping.Models;
using System.Web.ModelBinding;

namespace Oshopping.Controllers
{
    public class HomeController : ApiController
    {
        
        public IHttpActionResult GetALLdetails()
        {


            using (DatabasemodelEntities dc = new DatabasemodelEntities())
            {

                

                var Poducut = dc.Products.OrderBy(a => a.productname).ToList();

                // return Json(new { data = Product }, JsonRequestBehavior.AllowGet);

                return Ok(Poducut);

            }

        }

         //[HttpPost]  add  and update records
        public IHttpActionResult Save(Product Prod)
        {

            bool status = false;
            if (ModelState.IsValid)
            {
                using (DatabasemodelEntities dc = new DatabasemodelEntities())
                {
                    if (Prod.prodid > 0)
                    {
                        var v = dc.Products.Where(a => a.productname == Prod.productname).FirstOrDefault();
                        if (v != null)
                        {
                            v.productname = Prod.productname;
                            v.proditeam = Prod.proditeam;
                            v.Prodprice = Prod.Prodprice;


                        }

                        else
                        {

                            dc.Products.Add(Prod);


                            dc.SaveChanges();
                            status = true;
                        }



                    }
                    else
                    {
                        var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();//

                        return Ok("error");
                    }


                }
                
            }
            return Ok("ok");
        }



            public IHttpActionResult Delete(int prodid)
            {
                using (DatabasemodelEntities dc = new DatabasemodelEntities())
                {

                bool status = false;

                var v = dc.Products.Where(a => a.prodid == prodid).FirstOrDefault();
                    if (v != null)
                    {
                        dc.Products.Remove(v);
                        dc.SaveChanges();
                        status = true;

                    }


                }

                return Ok("Ok");

            }


        }
    
}
 