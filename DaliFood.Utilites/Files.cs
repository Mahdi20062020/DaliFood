using DaliFood.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaliFood.Utilites
{
    public static class Files
    {
        public static bool DeletePhotosProduct(UnitOfWork unitofwork,IEnumerable<Photo> photos,PhotoFor part)
        {
            try
            {
                foreach (var photo in photos)
                {
                    unitofwork.PhotoRepository.Delete(photo);
                    var savepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", part.PhotoSavedAddress, photo.Id.ToString() + photo.Extention);
                    System.IO.File.Delete(savepath);
                }
                unitofwork.PhotoRepository.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
           
        }
        public static bool ImportPhotosProduct(UnitOfWork unitofwork, IEnumerable<IFormFile> ImagesUpload, CustomersProduct CustomersProduct, PhotoFor part)
        {
            try
            {
                foreach (var ImageUpload in ImagesUpload)
                {
                    var photo = new Models.Photo() { PartId = part.Id, ItemId = CustomersProduct.Id, Extention = Path.GetExtension(ImageUpload.FileName) };
                    unitofwork.PhotoRepository.Create(photo);
                    int photoId = 0;
                    if (!unitofwork.PhotoRepository.GetAll().Any())
                    {
                        unitofwork.PhotoRepository.Save();
                        photoId = unitofwork.PhotoRepository.GetAll().Last().Id;

                    }
                    else
                    {
                        photoId = unitofwork.PhotoRepository.GetAll().Last().Id + 1;
                    }
                    SavePhoto(ImageUpload, part.PhotoSavedAddress, photoId.ToString() + photo.Extention);
                    //{
                    //    unitofwork.PhotoRepository.Delete(photoId);
                    //    unitofwork.PhotoRepository.Save();
                    //}
                    unitofwork.PhotoRepository.Save();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool SavePhoto(IFormFile ImageUpload, string Savepath,string filename)
        {
            try
            {
                var savepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", Savepath, filename);
                using (var Filestream = new FileStream(savepath, FileMode.Create))
                {
                    ImageUpload.CopyTo(Filestream);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            
        }
        public static IEnumerable<string> GetPhotoPaths(UnitOfWork unitofwork,int itemId, string partname,string scheme)
        {
            var part = unitofwork.PhotoForRepository.GetAll(where:p=>p.Name==partname).FirstOrDefault();
            var PhotoItems =unitofwork.PhotoRepository.GetAll(where: (p => p.ItemId == itemId && p.PartId == part.Id));
            List<string> savepathes = new List<string>();
            foreach (var PhotoItem in PhotoItems)
            {
                var savepath = Path.Combine(scheme, part.PhotoSavedAddress, PhotoItem.Id.ToString() + PhotoItem.Extention);
                savepathes.Add(savepath);
            }
            return savepathes;
        }
    }
}
