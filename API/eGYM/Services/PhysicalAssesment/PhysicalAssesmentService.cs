using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class PhysicalAssesmentService
    {
        private readonly EmployeeService employeeService;
        private readonly UserService userService;
        private readonly PhysicalAssesmentScheduledService scheduledAssesmentService;
        private readonly StudentRegistrationService studentRegistrationService;

        public PhysicalAssesmentService(PhysicalAssesmentRepository repository, EmployeeService employeeService, UserService userService,
            PhysicalAssesmentScheduledService scheduledAssesmentService, StudentRegistrationService studentRegistrationService) : this(repository)
        {
            this.employeeService = employeeService;
            this.userService = userService;
            this.scheduledAssesmentService = scheduledAssesmentService;
            this.studentRegistrationService = studentRegistrationService;
        }

        #region GetDataColumns()

        public override List<DataColumn> GetColumns()
        {
            List<DataColumn> dataColumns = new List<DataColumn>();
            dataColumns.Add(new DataColumn("student.user.name", DataTypes.String, "Nome do aluno"));
            dataColumns.Add(new DataColumn("registeredByEmployee.user.name", DataTypes.String, "Registrado por"));
            dataColumns.Add(new DataColumn("studentGoal", DataTypes.String, "Objetivo"));
            dataColumns.Add(new DataColumn("registerDateTime", DataTypes.Date, "Data de registro"));

            return dataColumns;
        }

        #endregion

        public override async Task PreSavingRoutine(PhysicalAssesment entity)
        {
            User user = await this.userService.ResolveUser();
            if (user == null)
            {
                throw new Exception("Não foi possivel encontrar o usuário atual.");
            }

            PhysicalAssesmentScheduled physicalAssesmentScheduled = await this.scheduledAssesmentService.GetByIdAsync((int)entity.ScheduledPhysicalAssesmentId);
            physicalAssesmentScheduled.WasAnswered = true;

            entity.ScheduledPhysicalAssesment = physicalAssesmentScheduled;
            entity.RegisterDateTime = DateTime.UtcNow.ToLocalTime();
            entity.RegisteredByEmployee = await this.employeeService.ResolveEmployeeByUser(user);
            entity.Student = await this.studentRegistrationService.GetByIdAsync(entity.StudentId);

            entity.StudentCaracteristics = await this.CalculateStudentCaracteristicsAsync(entity.StudentCaracteristics);
            entity.StudentCaracteristics.StudentRegistration = await this.studentRegistrationService.GetByIdAsync(entity.StudentCaracteristics.StudentRegistrationId);
        }

        public async Task<StudentCaracteristic> CalculateStudentCaracteristicsAsync(StudentCaracteristic studentCaracteristic)
        {
            StudentRegistration studentRegistration = await this.studentRegistrationService.GetByIdAsync(studentCaracteristic.StudentRegistrationId);

            if (Convert.ToInt32(studentRegistration.User.Genre) == (int)GenreEnum.Male)
            {
                //Homens utilizar a seguinte a fórmula: 66,5 + (13, 75 x Peso) +(5, 0 x Altura em cm) – (6, 8 x Idade).
                studentCaracteristic.BasalMetabolicRate = 66.5 + (13.75 * studentCaracteristic.Weight) + (5.0 * studentCaracteristic.Height) - (6.8 * studentCaracteristic.AgeAtMoment);
            }
            else
            {
                //Mulheres realizar a fórmula seguinte: 665,1 + (9, 56 x Peso) +(1, 8 x Altura em cm) – (4, 7 x Idade).
                studentCaracteristic.BasalMetabolicRate = 665.1 + (9.56 * studentCaracteristic.Weight) + (1.8 * studentCaracteristic.Height) - (4.7 * studentCaracteristic.AgeAtMoment);
            }

            double heightInMeters = (studentCaracteristic.Height / 100);
            studentCaracteristic.BodyMassIndex = studentCaracteristic.Weight / Math.Pow(heightInMeters, 2);

            //Para o cálculo da densidade corporal, utilizou-se a equação generalizada desenvolvida por Jackson &Pollock(1978) e validada por Petroski(1995),
            //no qual utiliza sete dobras cutâneas, idade e perímetro do abdome e do antebraço(QUADRO 1).
            //Cálculo da Densidade Corporal para 7 dobras(Pollock 7 dobras):
            //Dc(g / cm3) = 1,112 - 0,00043499 * (soma 7 Dobras)+0,00000055 * (soma 7 Dobras)2 - 0,00028826 * (Idade)
            double sevenSum = ((double)(studentCaracteristic.Thigh + studentCaracteristic.Suprailiac +
                                studentCaracteristic.Subscapular + studentCaracteristic.Subaxillary +
                                studentCaracteristic.Chest + studentCaracteristic.Abdominal + studentCaracteristic.Triceps));
            studentCaracteristic.BodyDensity = 1.112 - (0.00043499 * sevenSum) + (0.00000055 * Math.Pow(sevenSum, 2)) - (0.00028826 * studentCaracteristic.AgeAtMoment);

            //E O percentual de gordura(% G) para a técnica antropométrica foi estimado através da equação de SIRI(1961) % G = (495 / D) - 450, onde % G = percentual de gordura; D = densidade(g / ml).
            //Cálculo do percentual de Gordura Corporal:
            //G % = [(4, 95 / DENS) – 4, 50] x 100
            studentCaracteristic.FatPercentage = ((4.95 / studentCaracteristic.BodyDensity) - 4.5) * 100;
            // Gordura Absoluta(kg): = (peso corporal x % G) / 100
            studentCaracteristic.FatMass = (double)((studentCaracteristic.Weight * studentCaracteristic.FatPercentage) / 100);
            // Massa Magra(kg): = (peso corporal – Gordura Absoluta)
            studentCaracteristic.LeanMass = studentCaracteristic.Weight - studentCaracteristic.FatMass;

            return studentCaracteristic;
        }
    }
}