using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAppMVVM.Data;

public class HotelDbContextFactory
{
    private readonly string _connectionString;

    public HotelDbContextFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public HotelDbContext CreateDbContext()
    {
        DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;

        return new HotelDbContext(options);
    }
}
