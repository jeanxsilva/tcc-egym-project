Set-Location C:\Users\jeanx\Documents\Git\TCC-Projeto\e-GYM\API\eGYM\GeneratedModels
$list = Get-ChildItem -Name

foreach($file in $list){
 $name = $file.replace('.cs','');
 $entityName = $file.replace('.cs','');

 New-Item -Path ../Services -Name $name -ItemType "directory"
 New-Item -Path ../Services/$name -Name $entityName"Service.cs" -ItemType "file"
 
Add-Content -Path ../Services/$name/$entityName"Service.cs" -Value @"
using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class $($entityName)Service : ServiceBase<$($entityName), $($entityName)Repository>
    {
        public $($entityName)Service($($entityName)Repository repository)
        {
            this.Repository = repository;
        }
    }
}
"@
}
