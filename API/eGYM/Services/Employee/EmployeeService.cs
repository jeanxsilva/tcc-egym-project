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

        public EmployeeService(EmployeeRepository repository,
                UserService userService,
                UserLevelRepository userLevelRepository,
                UserStateRepository userStateRepository,
                UserProfileService userProfileService,
                ShiftService shiftService) : this(repository)
        {
            this.userService = userService;
            this.userLevelRepository = userLevelRepository;
            this.userStateRepository = userStateRepository;
            this.userProfileService = userProfileService;
            this.shiftService = shiftService;
        }


        #region GetDataColumns()

        public override List<DataColumn> GetColumns()
        {
            List<DataColumn> dataColumns = new List<DataColumn>();
            dataColumns.Add(new DataColumn("id", DataTypes.Int, "Id"));
            dataColumns.Add(new DataColumn("user.name", DataTypes.String, "Nome do aluno"));
            dataColumns.Add(new DataColumn("user.registerCode", DataTypes.String, "Código de registro"));
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
            UserProfile userProfile = this.userProfileService.GetUserProfileByUser(employeeUser);

            if (userProfile == null)
            {
                UserLevel userLevel = await this.userLevelRepository.GetById(2);
                UserState userState = await this.userStateRepository.GetById((long)UserStateEnum.Active);

                userProfile = new UserProfile();
                userProfile.Login = employeeUser.RegisterCode;
                userProfile.UserLevel = userLevel;
                userProfile.UserState = userState;
            }

            userProfile.Password = employeeUser.Birthday.ToString();

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