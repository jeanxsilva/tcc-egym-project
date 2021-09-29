Set-Location C:\Users\jeanx\Documents\Git\TCC-Projeto\e-GYM\API\eGYM\GeneratedModels

$list = Get-ChildItem -Name

foreach($file in $list){
 $name = $file.replace('.cs','');
 $entityName = $file.replace('.cs','');

 New-Item -Path  ../Repositories/ -Name $name -ItemType "directory"
 New-Item -Path ../Repositories/$name -Name $entityName"Repository.cs" -ItemType "file"

Add-Content -Path ../Repositories/$name/$entityName"Repository.cs" -Value @"
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class $($entityName)Repository : RepositoryBase<$($entityName)>
    {
        public $($entityName)Repository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public $($entityName)Repository()
        {
        }
    }
}
"@
}
