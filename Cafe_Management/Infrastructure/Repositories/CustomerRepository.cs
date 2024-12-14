using Cafe_Management.Code;
using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;
using Cafe_Management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cafe_Management.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;
        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Customer>> GetCustomers(Nullable<int>Customer_ID, Nullable<bool> IsActive, Nullable<int> Level_ID)
        {
            List<Customer>CustomerList = null;
            Expression<Func<Customer, bool>> _Filter = r => true;

            if (Customer_ID != null)
            {
                _Filter = Function.AndAlso(_Filter, x => x.Customer_Id ==Customer_ID);
            }

            if (IsActive != null)
            {
                _Filter = Function.AndAlso(_Filter, x => x.IsActive == IsActive);
            }

            if (Level_ID != null)
            {
                _Filter = Function.AndAlso(_Filter, x => x.Level_ID == Level_ID);
            }

            CustomerList = await _context.Customer.Where(_Filter).ToListAsync();

            return CustomerList;
        }


        public async Task AddCustomer(Customer customer)
        {
           customer.CreatedDate = DateTime.Now;
           customer.ModifiedDate = DateTime.Now;

            await _context.Customer.AddAsync(customer);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateCustomer(Customer customer)
        {
            var existingCustomer = await _context.Customer.FindAsync(customer.Customer_Id);
            if (existingCustomer != null)
            {
                if (customer.Customer_Name != null)
                {
                    existingCustomer.Customer_Name = customer.Customer_Name;
                }
                if (customer.Customer_Phone != null)
                {
                    existingCustomer.Customer_Phone = customer.Customer_Phone;
                }
                if (customer.Customer_Address != null)
                {
                    existingCustomer.Customer_Address = customer.Customer_Address;
                }
                if (customer.Customer_Email != null)
                {
                    existingCustomer.Customer_Email = customer.Customer_Email;
                }
                if (customer.Customer_Point != null)
                {
                    existingCustomer.Customer_Point = customer.Customer_Point;
                }
                if (customer.Level_ID != null)
                {
                    existingCustomer.Level_ID = customer.Level_ID;
                }
                if (customer.IsActive != null)
                {
                    existingCustomer.IsActive = customer.IsActive;
                }

                existingCustomer.ModifiedDate = DateTime.Now;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<Customer> getCustomerByUserName(string userName)
        {
            return await _context.Customer.Where(c => c.Username == userName).FirstOrDefaultAsync();
        }
    }
}
