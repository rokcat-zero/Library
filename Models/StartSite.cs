using Library.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class StartSite
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var sitecontext = new SiteContext(
                serviceProvider.GetRequiredService<DbContextOptions<SiteContext>>()))
            {
                /**                     *
                 * this is custom code  *
                 *                      *   
                 */
            }
        }

    }
}
