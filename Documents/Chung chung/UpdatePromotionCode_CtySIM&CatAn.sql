
-- update PromotionCode cho Exam của Nhân viên công ty SIM & Cát An
UPDATE exam.Exam
SET PromotionCode = '***'
WHERE ExamID in (SELECT ExamID 
				FROM exam.Exam
				WHERE PatientID in (SELECT PatientID 
									FROM pat.Patient
									WHERE [Address] like 'Công ty%' and [Address] not like 'Công ty CP%')
				and isDelete = 0)

