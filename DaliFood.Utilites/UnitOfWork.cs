using DaliFood.AdminPanel.Data;
using DaliFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DaliFood.Utilites
{
    public class UnitOfWork : IDisposable
    {
        ApplicationDbContext context;
        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }
        private Repository<Product> _ProductRepository;
        public Repository<Product> ProductRepository
        {
            get
            {
                if (_ProductRepository == null)
                {
                    _ProductRepository = new Repository<Product>(context);
                }
                return _ProductRepository;
            }
        }

        private Repository<ProductCategorie> _ProductCategorieRepository;
        public Repository<ProductCategorie> ProductCategorietRepository
        {
            get
            {
                if (_ProductCategorieRepository == null)
                {
                    _ProductCategorieRepository = new Repository<ProductCategorie>(context);
                }
                return _ProductCategorieRepository;
            }
        }

        private Repository<Restaurant> _RestaurantRepository;
        public Repository<Restaurant> RestaurantRepository
        {
            get
            {
                if (_RestaurantRepository == null)
                {
                    _RestaurantRepository = new Repository<Restaurant>(context);
                }
                return _RestaurantRepository;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
            }
        }
    }
}
