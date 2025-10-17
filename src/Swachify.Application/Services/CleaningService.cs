using Microsoft.EntityFrameworkCore;
using Swachify.Infrastructure.Data;
using Swachify.Infrastructure.Models;

namespace Swachify.Application;

public class CleaningService(MyDbContext db) : ICleaningService
{
}