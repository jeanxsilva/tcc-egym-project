using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class EmployeeService
    {
        private readonly UserService userService;
        private readonly UserLevelRepository userLevelRepository;
        private readonly UserStateRepository userStateRepository;
        private readonly UserProfileService userProfileService;
        private readonly ShiftService shiftService;
        private readonly CompanyUnitService companyUnitService;

        public EmployeeService(EmployeeRepository repository,
                UserService userService,
                UserLevelRepository userLevelRepository,
                UserStateRepository userStateRepository,
                UserProfileService userProfileService,
                ShiftService shiftService,
                CompanyUnitService companyUnitService) : this(repository)
        {
            this.userService = userService;
            this.userLevelRepository = userLevelRepository;
            this.userStateRepository = userStateRepository;
            this.userProfileService = userProfileService;
            this.shiftService = shiftService;
            this.companyUnitService = companyUnitService;
        }


        #region GetDataColumns()

        public override List<DataColumn> GetColumns()
        {
            List<DataColumn> dataColumns = new List<DataColumn>();

            dataColumns.Add(new DataColumn("user.name", DataTypes.String, "Nome"));
            dataColumns.Add(new DataColumn("user.registerCode", DataTypes.String, "Identificação"));
            dataColumns.Add(new DataColumn("user.userProfile.userLevel.description", DataTypes.String, "Nível"));
            dataColumns.Add(new DataColumn("shift.description", DataTypes.String, "Turno"));

            return dataColumns;
        }

        #endregion

        public override async Task PreSavingRoutine(Employee entity)
        {
            User user = await this.userService.ResolveUser();
            if (user == null)
            {
                throw new Exception("Não foi possivel encontrar o usuário atual.");
            }

            entity.Shift = await this.shiftService.GetByIdAsync(entity.ShiftId);

            User employeeUser = entity.User;
            employeeUser.CompanyUnit = await this.companyUnitService.ResolveCompanyUnit();
            UserProfile userProfile = this.userProfileService.GetUserProfileByUser(employeeUser);

            if (userProfile == null)
            {
                UserLevel userLevel = await this.userLevelRepository.GetById(entity.User.UserProfile.UserLevelId);
                UserState userState = await this.userStateRepository.GetById((int)UserStateEnum.Active);

                userProfile = new UserProfile();
                userProfile.Login = employeeUser.RegisterCode;
                userProfile.UserLevel = userLevel;
                userProfile.UserState = userState;
            }

            userProfile.Password = employeeUser.Birthday.ToString();
            userProfile.PasswordEncrypted = this.userProfileService.EncryptPassword(userProfile.Password);

            employeeUser.UserProfile = userProfile;
            entity.User = employeeUser;
        }

        public async Task<Employee> ResolveEmployeeByUser(User user)
        {
            IQueryable<Employee> queryable = this.Repository.GetQuery();
            Employee employee = queryable.FirstOrDefault(e => e.User.Id == user.Id);

            return employee;
        }
    }
}