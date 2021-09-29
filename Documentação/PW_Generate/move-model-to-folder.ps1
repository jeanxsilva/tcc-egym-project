Set-Location C:\Users\jeanx\Documents\Git\TCC-Projeto\e-GYM\API\eGYM\Models
$list = Get-ChildItem -Name
foreach($file in $list){
 $entityName = $file.replace('.cs','');
 
 (Get-Content ./$file).replace('public partial class '+ $entityName, 'public partial class ' + $entityName + ' : IEntityBase') | Set-Content ./$file
 
 New-Item -Path . -Name $entityName -ItemType "directory"
 Move-Item -Path ./$file -Destination ./$entityName/$file
}

# Set-Location C:\Users\jeanx\Documents\Git\TCC-Projeto\e-GYM\API\eGYM\Models
# $folders = Get-ChildItem -Name;
# foreach($folder in $folders){
#  Set-Location ./$folder;
#  $files = Get-ChildItem -Name;
#  foreach($file in $files){
#     $entityName = $file.replace('.cs', '');
#     (Get-Content ./$file).replace('public partial class '+ $entityName, 'public partial class ' + $entityName + ' : IEntityBase') | Set-Content ./$file
#  }
#  Set-Location ../;
# }