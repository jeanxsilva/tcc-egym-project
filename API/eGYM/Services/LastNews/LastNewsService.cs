using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class LastNewsService
    {
        private readonly UserService userService;

        public LastNewsService(LastNewsRepository repository, UserService userService) : this(repository)
        {
            this.userService = userService;
        }

        public override async Task PreSavingRoutine(LastNews entity)
        {
            entity.RegisterDateTime = DateTime.UtcNow.ToLocalTime();
            entity.PublishedByUser = await this.userService.ResolveUser();
        }
    }
}
