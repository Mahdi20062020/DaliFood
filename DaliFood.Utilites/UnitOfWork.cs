using DaliFood.Models;
using DaliFood.Models.Data;
using DaliFood.Models.Identity;
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

        private Repository<CustomersProduct> _CustomersProductRepository;
        public Repository<CustomersProduct> CustomersProductRepository
        {
            get
            {
                if (_CustomersProductRepository == null)
                {
                    _CustomersProductRepository = new Repository<CustomersProduct>(context);
                }
                return _CustomersProductRepository;
            }
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
        
        
        private Repository<PhotoFor> _PhotoForRepository;
        public Repository<PhotoFor> PhotoForRepository
        {
            get
            {
                if (_PhotoForRepository == null)
                {
                    _PhotoForRepository = new Repository<PhotoFor>(context);
                }
                return _PhotoForRepository;
            }
        }

      
        private Repository<Photo> _PhotoRepository;
        public Repository<Photo> PhotoRepository
        {
            get
            {
                if (_PhotoRepository == null)
                {
                    _PhotoRepository = new Repository<Photo>(context);
                }
                return _PhotoRepository;
            }
        }

        
        private Repository<Order> _OrderRepository;
        public Repository<Order> OrderRepository
        {
            get
            {
                if (_OrderRepository == null)
                {
                    _OrderRepository = new Repository<Order>(context);
                }
                return _OrderRepository;
            }
        }

        
        private Repository<OrderItem> _OrderItemRepository;
        public Repository<OrderItem> OrderItemRepository
        {
            get
            {
                if (_OrderItemRepository == null)
                {
                    _OrderItemRepository = new Repository<OrderItem>(context);
                }
                return _OrderItemRepository;
            }
        }


        private Repository<PhoneNumbersToken> _PhoneNumbersTokenRepository;
        public Repository<PhoneNumbersToken> PhoneNumbersTokenRepository
        {
            get
            {
                if (_PhoneNumbersTokenRepository == null)
                {
                    _PhoneNumbersTokenRepository = new Repository<PhoneNumbersToken>(context);
                }
                return _PhoneNumbersTokenRepository;
            }
        }


        private Repository<Address> _AddressRepository;
        public Repository<Address> AddressRepository
        {
            get
            {
                if (_AddressRepository == null)
                {
                    _AddressRepository = new Repository<Address>(context);
                }
                return _AddressRepository;
            }
        }


        private Repository<Deposit> _DepositRepository;
        public Repository<Deposit> DepositRepository
        {
            get
            {
                if (_DepositRepository == null)
                {
                    _DepositRepository = new Repository<Deposit>(context);
                }
                return _DepositRepository;
            }
        }


        private Repository<Withdraw> _WithdrawRepository;
        public Repository<Withdraw> WithdrawRepository
        {
            get
            {
                if (_WithdrawRepository == null)
                {
                    _WithdrawRepository = new Repository<Withdraw>(context);
                }
                return _WithdrawRepository;
            }
        }


        private Repository<Transaction> _TransactionRepository;
        public Repository<Transaction> TransactionRepository
        {
            get
            {
                if (_TransactionRepository == null)
                {
                    _TransactionRepository = new Repository<Transaction>(context);
                }
                return _TransactionRepository;
            }
        }


        private Repository<Providence> _ProvidenceRepository;
        public Repository<Providence> ProvidenceRepository
        {
            get
            {
                if (ProvidenceRepository == null)
                {
                    _ProvidenceRepository = new Repository<Providence>(context);
                }
                return _ProvidenceRepository;
            }
        }


        private Repository<City> _CityRepository;
        public Repository<City> CityRepository
        {
            get
            {
                if (_CityRepository == null)
                {
                    _CityRepository = new Repository<City>(context);
                }
                return _CityRepository;
            }
        }
        private Repository<CustomerComment> _CustomerCommentRepository;
        public Repository<CustomerComment> CustomerCommentRepository
        {
            get
            {
                if (_CustomerCommentRepository == null)
                {
                    _CustomerCommentRepository = new Repository<CustomerComment>(context);
                }
                return _CustomerCommentRepository;
            }
        }
        private Repository<Favorite> _FavoriteRepository;
        public Repository<Favorite> FavoriteRepository
        {
            get
            {
                if (_FavoriteRepository == null)
                {
                    _FavoriteRepository = new Repository<Favorite>(context);
                }
                return _FavoriteRepository;
            }
        } 
        private Repository<ApplicationNormalUser> _ApplicationNormalUserRepository;
        public Repository<ApplicationNormalUser> ApplicationNormalUserRepository
        {
            get
            {
                if (_ApplicationNormalUserRepository == null)
                {
                    _ApplicationNormalUserRepository = new Repository<ApplicationNormalUser>(context);
                }
                return _ApplicationNormalUserRepository;
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
