MODULE Module1
	CONST jointtarget JointTarget_5:=[[-28.3,-40.6,17.5,11,19.4,34.2],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_1:=[[91.9,-20.9,10.9,1.6,7.4,39.1],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_2:=[[88.9,6.9,-14.2,0.6,3.2,43.7],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_3:=[[88.9,6.3,-15.9,0.6,5.6,43.7],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_4:=[[85.3,-40.6,17.5,11,19.4,34.2],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_8:=[[26,64.1,-23.9,98.3,-104.1,-5.1],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_6:=[[17,87.1,-34.7,107.7,-93.6,7.7],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_9:=[[27.7,99.7,-68.8,114.5,-106.3,-9.5],[9E9,9E9,9E9,9E9,9E9,9E9]];
PROC No_6_Tray()
	Set DO10_4;
	Reset DO10_5;
	MoveAbsJ JointTarget_5,v1000,fine,tool0;
	MoveAbsJ JointTarget_1,v1000,fine,tool0;
	MoveAbsJ JointTarget_2,v1000,fine,tool0;
	Set DO10_5;
	Reset DO10_4;
	MoveAbsJ JointTarget_3,v1000,fine,tool0;
	MoveAbsJ JointTarget_4,v1000,fine,tool0;
	MoveAbsJ JointTarget_5,v1000,fine,tool0;
	MoveAbsJ JointTarget_8,v1000,fine,tool0;
	MoveAbsJ JointTarget_6,v1000,fine,tool0;
	Set DO10_4;
	Reset DO10_5;
	MoveAbsJ JointTarget_9,v1000,z100,tool0;
	MoveAbsJ JointTarget_8,v1000,fine,tool0;
ENDPROC
PROC main()
	No_6_Tray;
ENDPROC
PROC No_5_Tray()
ENDPROC



ENDMODULE