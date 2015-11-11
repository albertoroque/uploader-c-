using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UploadImage.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string cam, string msg)
        {
            if (cam == null)
            {
                ViewBag.Image = "http://www.datastax.com/wp-content/plugins/all-in-one-seo-pack/images/default-user-image.png";
            }
            else
            {
                ViewBag.Image = cam;
            }

            ViewBag.Msg = msg;
            return View();
        }

        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase file)
        {            
            string cam = null;
            string msg = null;
            if (file != null)
            {
                //ERRO SE FOTO MAIOR QUE 3 mbytes
                if (file.ContentLength > 3200000)
                {

                    return RedirectToAction("Index", "Home", new { cam = String.Empty, msg = "Imagem maior que 3mbs" });
                }
                                                
                string extension = System.IO.Path.GetExtension(file.FileName);

                if (!extension.Equals(".jpg") && !extension.Equals(".png"))
                {
                    return RedirectToAction("Index", "Home", new { cam = String.Empty, msg = "Tipo de arquivo não suportado" });
                }

                string g = Guid.NewGuid().ToString("N");

                string filename = g + extension;
                string path = System.IO.Path.Combine(Server.MapPath("~/images/profile"), filename);
                cam = ("../images/profile/" + filename);
                
                file.SaveAs(path);                            
            }

            return RedirectToAction("Index", "Home", new { cam = cam, msg = "Sucesso!" });
        }
            
    }
}