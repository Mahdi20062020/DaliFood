using DaliFood.Models;
using Microsoft.AspNetCore.Http;
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
                var savepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","Images",Savepath, filename);
                using (var Filestream = new FileStream(savepath, FileMode.Create))
                {
                    ImageUpload.CopyTo(Filestream);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
