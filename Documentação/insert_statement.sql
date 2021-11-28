INSERT INTO `egym_db`.`company` (`Description`,`RegisterCode`)
	VALUES
    ("New Sphere Gym","1112369520001");
INSERT INTO `egym_db`.`company_unit` (`Description`, `CompanyId`,`RegisterCode`,`PostalCode`,`Phone`,`Email`,`UserContactId`) 
	VALUES ("New Sphere Curitiba",1,"1114522233355","81460000","4133334444","newsphere@administrativo.com", null);
    
INSERT INTO `egym_db`.`user`(`Id`,`RegisterCode`,`Name`,`LastName`,`Birthday`,`Email`,`Phone`,`CompanyUnitId`,`Genre`, `AddressCode`)
	VALUES
	(1, "11111111111","Administrador","", "04/10/2021","admin@newsphere.com","4199998888", 1,true, "81460000");
    
INSERT INTO `egym_db`.`user_level`(`Id`,`Description`, `RoleCode`)
	VALUES
	(99, "Administrador", "admin"),
	(55, "Aluno", "student"),
	(1, "Instrutor", "instructor"),
	(10, "Gestor", "manager"),
	(2, "Recepcionista", "recepcionist");
    
INSERT INTO `egym_db`.`user_state`(`Id`,`Description`)
	VALUES
	(1, "Ativo"),
	(0, "Inativo");
    
INSERT INTO `egym_db`.`user_level_access`(`Id`,`Description`,`Path`,`IconKey`,`UserLevelId`,`HasChild`,`ParentId`)
	VALUES
	(1, "Dashboard", "/dashboard", "fas fa-tachometer-alt", 10, false, null),
    
	(2, "Matriculas", "/registration","fas fa-list", 10, false, null),
    
	(3, "Gerenciar avisos", "","fas fa-newspaper", 10, true, null),
	(4, "Ultimos avisos", "/last-news","fas fa-list", 10, false, 3),
	(5, "Registrar novo aviso", "/news/register","fas fa-plus text-success", 10, false, 3),
    
	(6, "Avaliação fisica", "","fas fa-user-md", 10, true, null),
	(7, "Avaliações agendadas", "/assessment/scheduleds", "far fa-calendar-alt", 10, false, 6),
	(8, "Histórico de avaliações", "/assessments", "fas fa-list", 10, false, 6),
	(9, "Registrar novo agendamento", "/assessment/schedule/register", "fas fa-plus text-success", 10, false, 6),
    
	(10, "Pagamentos", "", "fas fa-file-invoice-dollar", 10, true, null),
	(11, "Histórico de pagamentos", "/payments", "fas fa-list", 10, false, 10),
	(12, "Histórico de estornos", "/payment/reversals", "fas fa-list", 10, false, 10),
	(13, "Listar faturas", "/payment/invoices", "fas fa-list", 10, false, 10),
    
	(14, "Gerenciar funcionários", "", "fas fa-id-card-alt", 10, true, null),
	(15, "Listar funcionários", "/employees", "fas fa-list", 10, false, 14),
	(16, "Registrar novo funcionário", "/employee/create", "fas fa-plus text-success", 10, false, 14),
    
	(17, "Gerenciar alunos", "", "far fa-address-book", 10, true, null),
	(18, "Listar alunos", "/students", "fas fa-list", 10, false, 17),
	(19, "Registrar novo aluno", "/student/create", "fas fa-plus text-success", 10, false, 17),
    
	(20, "Gerenciar treinos", "", "fas fa-dumbbell", 10, true, null),
	(21, "Listar treinos", "/trainings", "fas fa-list", 10, false, 20),
	(22, "Registrar novo treino", "/training/create", "fas fa-plus text-success", 10, false, 20),
    
	(23, "Gerenciar exercicios", "", "fas fa-dumbbell", 10, true, null),
	(24, "Listar exercicios", "/exercises", "fas fa-list", 10, false, 23),
	(25, "Registrar novo exercicio", "/exercise/create", "fas fa-plus text-success", 10, false, 23),
    
	(26, "Gerenciar modalidades", "", "fas fa-tasks", 10, true, null),
	(27, "Listar modalidades", "/modalities", "fas fa-list", 10, false, 26),
	(28, "Registrar nova modalidade", "/modality/create", "fas fa-plus text-success", 10, false, 26),
    
	(29, "Gerenciar turmas", "", "fas fa-users", 10, true, null),
	(30, "Listar turmas", "/modality/classes", "fas fa-list", 10, false, 29),
	(31, "Registrar nova turma", "/modality/class/create", "fas fa-plus text-success", 10, false,29),
		
	('32', 'Dashboard', '/dashboard', 'fas fa-tachometer-alt', '1', '0', NULL),
	('33', 'Matriculas', '/registration', 'fas fa-list', '1', '0', NULL),
	('34', 'Ultimos avisos', '/last-news', 'fas fa-list', '1', '0', NULL),
	('35', 'Avaliação fisica', '', 'fas fa-user-md', '1', '1', NULL),
	('36', 'Avaliações agendadas', '/assessment/scheduleds', 'far fa-calendar-alt', '1', '0', '35'),
	('37', 'Histórico de avaliações', '/assessments', 'fas fa-list', '1', '0', '35'),
	('38', 'Registrar novo agendamento', '/assessment/schedule/register', 'fas fa-plus text-success', '1', '0', '35'),
	('39', 'Pagamentos', '', 'fas fa-file-invoice-dollar', '1', '1', NULL),
	('40', 'Histórico de pagamentos', '/payments', 'fas fa-list', '1', '0', '39'),
	('41', 'Histórico de estornos', '/payment/reversals', 'fas fa-list', '1', '0', '39'),
	('42', 'Listar faturas', '/payment/invoices', 'fas fa-list', '1', '0', '39'),
	('43', 'Gerenciar alunos', '', 'far fa-address-book', '1', '1', NULL),
	('44', 'Listar alunos', '/students', 'fas fa-list', '1', '0', '43'),
	('45', 'Registrar novo aluno', '/student/create', 'fas fa-plus text-success', '1', '0', '43'),
	('46', 'Gerenciar treinos', '', 'fas fa-dumbbell', '1', '1', NULL),
	('47', 'Listar treinos', '/trainings', 'fas fa-list', '1', '0', '46'),
	('48', 'Registrar novo treino', '/training/create', 'fas fa-plus text-success', '1', '0', '46'),
	('49', 'Dashboard', '/dashboard', 'fas fa-tachometer-alt', '2', '0', NULL),
	('50', 'Matriculas', '/registration', 'fas fa-list', '2', '0', NULL),
	('51', 'Ultimos avisos', '/last-news', 'fas fa-list', '2', '0',  NULL),
	('52', 'Pagamentos', '', 'fas fa-file-invoice-dollar', '2', '1', NULL),
	('53', 'Histórico de pagamentos', '/payments', 'fas fa-list', '2', '0', '52'),
	('54', 'Histórico de estornos', '/payment/reversals', 'fas fa-list', '2', '0', '52'),
	('55', 'Listar faturas', '/payment/invoices', 'fas fa-list', '2', '0', '52'),
	('56', 'Gerenciar alunos', '', 'far fa-address-book', '2', '1', NULL),
	('57', 'Listar alunos', '/students', 'fas fa-list', '2', '0', '57'),
	('58', 'Registrar novo aluno', '/student/create', 'fas fa-plus text-success', '2', '0', '57'),
		
	('59', 'Dashboard', '/dashboard', 'fas fa-tachometer-alt', '99', '0', NULL),
	('60', 'Matriculas', '/registration', 'fas fa-list', '99', '0', NULL),
	('61', 'Gerenciar avisos', '', 'fas fa-newspaper', '99', '1', NULL),
	('62', 'Ultimos avisos', '/last-news', 'fas fa-list', '99', '0', '61'),
	('63', 'Registrar novo aviso', '/news/register', 'fas fa-plus text-success', '99', '0', '61'),
	('64', 'Avaliação fisica', '', 'fas fa-user-md', '99', '1', NULL),
	('65', 'Avaliações agendadas', '/assessment/scheduleds', 'far fa-calendar-alt', '99', '0', '64'),
	('66', 'Histórico de avaliações', '/assessments', 'fas fa-list', '99', '0', '64'),
	('67', 'Registrar novo agendamento', '/assessment/schedule/register', 'fas fa-plus text-success', '99', '0', '64'),
	('68', 'Pagamentos', '', 'fas fa-file-invoice-dollar', '99', '1', NULL),
	('69', 'Histórico de pagamentos', '/payments', 'fas fa-list', '99', '0', '68'),
	('70', 'Histórico de estornos', '/payment/reversals', 'fas fa-list', '99', '0', '68'),
	('71', 'Listar faturas', '/payment/invoices', 'fas fa-list', '99', '0', '68'),
	('72', 'Gerenciar funcionários', '', 'fas fa-id-card-alt', '99', '1', NULL),
	('73', 'Listar funcionários', '/employees', 'fas fa-list', '99', '0', '72'),
	('74', 'Registrar novo funcionário', '/employee/create', 'fas fa-plus text-success', '99', '0', '72'),
	('75', 'Gerenciar alunos', '', 'far fa-address-book', '99', '1', NULL),
	('76', 'Listar alunos', '/students', 'fas fa-list', '99', '0', '75'),
	('77', 'Registrar novo aluno', '/student/create', 'fas fa-plus text-success', '99', '0', '75'),
	('78', 'Gerenciar treinos', '', 'fas fa-dumbbell', '99', '1', NULL),
	('79', 'Listar treinos', '/trainings', 'fas fa-list', '99', '0', '78'),
	('80', 'Registrar novo treino', '/training/create', 'fas fa-plus text-success', '99', '0', '78'),
	('81', 'Gerenciar exercicios', '', 'fas fa-dumbbell', '99', '1', NULL),
	('82', 'Listar exercicios', '/exercises', 'fas fa-list', '99', '0', '81'),
	('83', 'Registrar novo exercicio', '/exercise/create', 'fas fa-plus text-success', '99', '0', '81'),
	('84', 'Gerenciar modalidades', '', 'fas fa-tasks', '99', '1', NULL),
	('85', 'Listar modalidades', '/modalities', 'fas fa-list', '99', '0', '84'),
	('86', 'Registrar nova modalidade', '/modality/create', 'fas fa-plus text-success', '99', '0', '84'),
	('87', 'Gerenciar turmas', '', 'fas fa-users', '99', '1', NULL),
	('88', 'Listar turmas', '/modality/classes', 'fas fa-list', '99', '0', '87'),
	('89', 'Registrar nova turma', '/modality/class/create', 'fas fa-plus text-success', '99', '0', '87'),
	(90, "Listar alunos por modalidade", "/modality/create", "fas fa-plus text-success", 10, false, 26),
	(91, "Listar alunos por modalidade", "/modality/students", "fas fa-list", 99, false, 84);
    
INSERT INTO `egym_db`.`user_level_roles`(`Role`,`UserLevelId`)
	VALUES
		('ClassCheckInOut.C', 99),
		('ClassCheckInOut.R', 99),
		('ClassCheckInOut.U', 99),
		('ClassCheckInOut.D', 99),
		('Company.C', 99),
		('Company.R', 99),
		('Company.U', 99),
		('Company.D', 99),
		('CompanyUnit.C', 99),
		('CompanyUnit.R', 99),
		('CompanyUnit.U', 99),
		('CompanyUnit.D', 99),
		('Employee.C', 99),
		('Employee.R', 99),
		('Employee.U', 99),
		('Employee.D', 99),
		('Exercise.C', 99),
		('Exercise.R', 99),
		('Exercise.U', 99),
		('Exercise.D', 99),
		('ExerciseCategory.C', 99),
		('ExerciseCategory.R', 99),
		('ExerciseCategory.U', 99),
		('ExerciseCategory.D', 99),
		('Invoice.C', 99),
		('Invoice.R', 99),
		('Invoice.U', 99),
		('Invoice.D', 99),
		('InvoiceDetail.C', 99),
		('InvoiceDetail.R', 99),
		('InvoiceDetail.U', 99),
		('InvoiceDetail.D', 99),
		('InvoiceStatus.C', 99),
		('InvoiceStatus.R', 99),
		('InvoiceStatus.U', 99),
		('InvoiceStatus.D', 99),
		('LastNews.C', 99),
		('LastNews.R', 99),
		('LastNews.U', 99),
		('LastNews.D', 99),
		('Modality.C', 99),
		('Modality.R', 99),
		('Modality.U', 99),
		('Modality.D', 99),
		('ModalityClass.C', 99),
		('ModalityClass.R', 99),
		('ModalityClass.U', 99),
		('ModalityClass.D', 99),
		('ModalityPaymentType.C', 99),
		('ModalityPaymentType.R', 99),
		('ModalityPaymentType.U', 99),
		('ModalityPaymentType.D', 99),
		('Payment.C', 99),
		('Payment.R', 99),
		('Payment.U', 99),
		('Payment.D', 99),
		('PaymentMovement.C', 99),
		('PaymentMovement.R', 99),
		('PaymentMovement.U', 99),
		('PaymentMovement.D', 99),
		('PaymentReversal.C', 99),
		('PaymentReversal.R', 99),
		('PaymentReversal.U', 99),
		('PaymentReversal.D', 99),
		('PaymentReversalStatus.C', 99),
		('PaymentReversalStatus.R', 99),
		('PaymentReversalStatus.U', 99),
		('PaymentReversalStatus.D', 99),
		('PaymentType.C', 99),
		('PaymentType.R', 99),
		('PaymentType.U', 99),
		('PaymentType.D', 99),
		('PhysicalAssesment.C', 99),
		('PhysicalAssesment.R', 99),
		('PhysicalAssesment.U', 99),
		('PhysicalAssesment.D', 99),
		('PhysicalAssesmentScheduled.C', 99),
		('PhysicalAssesmentScheduled.R', 99),
		('PhysicalAssesmentScheduled.U', 99),
		('PhysicalAssesmentScheduled.D', 99),
		('RegistrationModalityClass.C', 99),
		('RegistrationModalityClass.R', 99),
		('RegistrationModalityClass.U', 99),
		('RegistrationModalityClass.D', 99),
		('RequestCategory.C', 99),
		('RequestCategory.R', 99),
		('RequestCategory.U', 99),
		('RequestCategory.D', 99),
		('RequestStatus.C', 99),
		('RequestStatus.R', 99),
		('RequestStatus.U', 99),
		('RequestStatus.D', 99),
		('Shift.C', 99),
		('Shift.R', 99),
		('Shift.U', 99),
		('Shift.D', 99),
		('ShiftBook.C', 99),
		('ShiftBook.R', 99),
		('ShiftBook.U', 99),
		('ShiftBook.D', 99),
		('StudentCaracteristic.C', 99),
		('StudentCaracteristic.R', 99),
		('StudentCaracteristic.U', 99),
		('StudentCaracteristic.D', 99),
		('StudentRegistration.C', 99),
		('StudentRegistration.R', 99),
		('StudentRegistration.U', 99),
		('StudentRegistration.D', 99),
		('StudentRequest.C', 99),
		('StudentRequest.R', 99),
		('StudentRequest.U', 99),
		('StudentRequest.D', 99),
		('TrainingPlan.C', 99),
		('TrainingPlan.R', 99),
		('TrainingPlan.U', 99),
		('TrainingPlan.D', 99),
		('TrainingPlanExercise.C', 99),
		('TrainingPlanExercise.R', 99),
		('TrainingPlanExercise.U', 99),
		('TrainingPlanExercise.D', 99),
		('User.C', 99),
		('User.R', 99),
		('User.U', 99),
		('User.D', 99),
		('UserLevel.C', 99),
		('UserLevel.R', 99),
		('UserLevel.U', 99),
		('UserLevel.D', 99),
		('UserLevelAccess.C', 99),
		('UserLevelAccess.R', 99),
		('UserLevelAccess.U', 99),
		('UserLevelAccess.D', 99),
		('UserLevelRole.C', 99),
		('UserLevelRole.R', 99),
		('UserLevelRole.U', 99),
		('UserLevelRole.D', 99),
		('UserProfile.C', 99),
		('UserProfile.R', 99),
		('UserProfile.U', 99),
		('UserProfile.D', 99),
		('UserState.C', 99),
		('UserState.R', 99),
		('UserState.U', 99),
		('UserState.D', 99);
        
INSERT INTO `egym_db`.`user_profile`(`Login`,`PasswordEncrypted`,`UserId`,`UserLevelId`,`UserStateId`)
	VALUES
	("admin@newsphere.com","8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92","1",99,1);
    
INSERT INTO `egym_db`.`shift`(`Id`,`Description`,`StartTime`,`EndTime`)
	VALUES
    (1, "Primeiro turno","06:00:00","14:00:00"),
    (2, "Segundo turno","14:00:00","22:00:00");
    
INSERT INTO `egym_db`.`employee` (`Id`, `UserId`, `ShiftId`)
	VALUES (1, 1, 1);
    
INSERT INTO `egym_db`.`exercise_category`(`Id`,`Description`)
	VALUES
	(1, "Peito"),
	(2, "Costas"),
    (3, "Pernas"),
	(4, "Triceps"),
	(5, "Abdômen"),
    (6, "Biceps"),
    (7, "Ombros");
    
INSERT INTO `egym_db`.`exercise`(`Description`,`ExerciseCategoryId`)
	VALUES
	("Supino Reto Barra", 1),
	("Crucifixo", 1),
	("Supino Articulado Declinado", 1),
	("Voador", 1),
	("Supino Inclinado", 1),
	("Pullover", 1),
	("Triceps Inverso", 4),
	("Triceps Testa", 4),
	("Triceps Banco", 4),
	("Triceps Francês", 4),
	("Remador", 5),
	("Roda", 5),
    ("Pulley Costa", 2),
	("Voador Inverso", 2),
	("Pulley Frontal ", 2),
	("Remada Cavalinho", 2),
	("Remada Articulada", 2),
	("Curvada", 2),
	("Rosca Direta", 6),
	("Rosca Martelo", 6),
	("Rosca Scott", 6),
	("Fura Bucho", 5),
	("Inversa", 5),
	("Banco Declinado", 5),
    
	("Agachamento ", 3),
	("Cadeira Flexora ", 3),
	("Cama Flexora", 3),
	("Leg 45 Alto", 3),
	("Abdutor", 3),
	("Adutor", 3),
	("Extensora", 3),
	("Passada", 3),
	("Panturrilha Leg 45°", 3),
	("Abdominal Livre", 5),
	("Desenvolvimento c/ Halteres", 7),
	("Remada Alta", 7),
	("Elevação Lateral Cross", 7),
	("Desenvolvimento Frontal ", 7),
	("Elevação Frontal", 7),
	("Crucifixo Inverso ", 7),
	("Encolhimento", 7),
	("Puxada Alta", 7),
	("Infra Alternado ", 5),
	("Infra Solo Step", 5);
    
INSERT INTO `egym_db`.`invoice_status`(`Id`,`Description`)
	VALUES
	(0, "Gerada"),
	(5, "Paga"),
	(10, "Cancelada");
INSERT INTO `egym_db`.`modalities`(`Id`,`Description`,`Price`,`DaysInWeek`)
	VALUES
	(1, "Academia 6 dias por semana", "75.00", 6),
	(2, "Jiu jitsu", "95", 4);
INSERT INTO `egym_db`.`modality_payment_type`(`Id`,`Description`)
	VALUES
	(1, "Mensal"),
	(2, "Semestral"),
	(3, "Anual");
INSERT INTO `egym_db`.`modality_class` (`ModalityId`,`StartTime`,`EndTime`,`InstructorId`,`TotalVacancies`,`TotalActiveMembers`,`CompanyUnitId`)
	VALUES
	(1,"19:00:00","20:00:00",1, 30, 0, 1),
	(2,"06:00:00","23:00:00",1, 999, 0, 1);

INSERT INTO `egym_db`.`payment_reversal_status`(`Id`, `Description`)
	VALUES
	(0, "Aberto"),
	(5, "Movimentado"),
	(10, "Deferido"),
	(15, "Recusado"),
	(20, "Cancelado");
INSERT INTO `egym_db`.`payment_type`(`Id`,`Description`)
	VALUES
	(0, "Boleto"),
	(1, "Dinheiro"),
	(2, "Cartão de crédito"),
	(3, "Cartão de débito");
INSERT INTO `egym_db`.`request_status`(`Id`, `Description`)
	VALUES
	(0, "Aberto"),
	(5, "Deferido"),
	(10, "Recusado"),
	(15, "Cancelado");
INSERT INTO `egym_db`.`request_category_level` (`UserLevelId`, `RequestCategoryId`)
VALUES 
	('99', '1'), 
	('99', '2'), 
	('99', '3'),
	(1,2),
	(1,3),
	(10,1),
	(10,2),
	(10,3),
	(2,2),
	(2,3);
    
/* TESTES */
INSERT INTO `egym_db`.`student_registration` (`Id`,`UserId`,`Code`,`ActualTrainingPlanId`,`RegisterDateTime`) 
	VALUES ('1', '1', 'Teste', NULL, '2029-10-21 00:00:00');
    
INSERT INTO `egym_db`.`invoice` (`Id`,`TotalValue`,`ReferentToDate`,`DueDate`,`InvoiceStatusId`,`CompanyUnitId`,`StudentId`,`Note`,`IsByRequest`)
	VALUES ('1', '90', '2021-10-29', '2021-10-30', '0', '1', '1', NULL, '0');

INSERT INTO `egym_db`.`payment`(`Id`,`IsValid`,`PaymentTypeId`,`PaymentDateTime`,`InvoiceId`,`PaidByUserId`,`CompanyUnitId`,`ReceivedByUserId`) 
	VALUES ('1', 1, '1', '2021-10-29 00:00:00', '1', '1', '1', '1');
INSERT INTO `egym_db`.`payment_reversal` (`Id`,`Reason`,`AuthorizedByUserId`,`CreatedByUserId`,`FinishedByUserId`,`PaymentId`,`PaymentReversalStatusId`,`LastModifiedDateTime`) 
	VALUES ('1', 'Teste', '1', '1', null, '1', '0', '2021-10-29 00:00:00');
INSERT INTO `egym_db`.`payment_movements`(`Id`,`RegisterDateTime`,`IsCurrent`,`PaymentReversalStatusId`,`RegisteredByUserId`,`Note`,`PaymentReversalId`) 
	VALUES (1, '2021/10/30', 0, 0, 1, 'Teste 1', 1),
		(2, '2021/10/30', 1, 5, 1, 'Teste 2', 1);
