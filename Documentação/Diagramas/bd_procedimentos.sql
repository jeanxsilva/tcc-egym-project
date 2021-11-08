DELIMITER $$
DROP PROCEDURE IF EXISTS ClassChangeActiveMembers;
CREATE PROCEDURE ClassChangeActiveMembers(modalityClassId int(11))
BEGIN
	UPDATE modality_class 
		SET TotalActiveMembers = (select count(*) from registration_modality_class rmc where rmc.ModalityClassId = modalityClassId AND rmc.IsValid = true)
	WHERE 
		Id = modalityClassId;
END $$

DELIMITER ;
DROP TRIGGER IF EXISTS Tgr_Registration_Insert;

DELIMITER $
CREATE TRIGGER Tgr_Registration_Insert AFTER INSERT
ON registration_modality_class
FOR EACH ROW
BEGIN
	CALL ClassChangeActiveMembers(NEW.ModalityClassId);
END$

DELIMITER ;
DROP TRIGGER IF EXISTS Tgr_Registration_Update;

DELIMITER $
CREATE TRIGGER Tgr_Registration_Update AFTER UPDATE
ON registration_modality_class
FOR EACH ROW
BEGIN
	CALL ClassChangeActiveMembers(NEW.ModalityClassId);
END$

DELIMITER ;
DROP TRIGGER IF EXISTS Tgr_Registration_Delete;

DELIMITER $
CREATE TRIGGER Tgr_Registration_Delete AFTER DELETE
ON registration_modality_class
FOR EACH ROW
BEGIN
	CALL ClassChangeActiveMembers(OLD.ModalityClassId);
END$

DELIMITER ;