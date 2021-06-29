using DaliFood.Models;
using DaliFood.Models.Data;
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
        public Repository<ProductCategorie> ProductCategorieRepository
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
        private Repository<CustomerType> _CustomerTypeRepository;
        public Repository<CustomerType> CustomerTypeRepository
        {
            get
            {
                if (_CustomerTypeRepository == null)
                {
                    _CustomerTypeRepository = new Repository<CustomerType>(context);
                }
                return _CustomerTypeRepository;
            }
        }
        private Repository<Customer> _CustomerRepository;
        public Repository<Customer> CustomerRepository
        {
            get
            {
                if (_CustomerRepository == null)
                {
                    _CustomerRepository = new Repository<Customer>(context);
                }
                return _CustomerRepository;
            }
        }
        private Repository<Discount> _DiscountRepository;
        public Repository<Discount> DiscountRepository
        {
            get
            {
                if (_DiscountRepository == null)
                {
                    _DiscountRepository = new Repository<Discount>(context);
                }
                return _DiscountRepository;
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
