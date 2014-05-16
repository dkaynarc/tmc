MODULE Module1
	CONST jointtarget JointTarget_5:=[[-28.3,-40.6,17.5,11,19.4,34.2],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_1:=[[91.2,-27.8,16.7,10.9,8.3,30.6],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_7:=[[90.1,-12.3,5.9,-0.4,1.9,44.5],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_2:=[[89.8,6,-11.9,-2.8,0.9,47.6],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_3:=[[88.9,6.3,-15.9,0.6,5.6,43.7],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_4:=[[85.3,-40.6,17.5,11,19.4,34.2],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_8:=[[26,64.1,-23.9,98.3,-104.1,-5.1],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_11:=[[17.7,78.1,-21.8,108.6,-92.7,11.7],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_10:=[[17.7,82.7,-25.3,108.6,-92.4,12.7],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_9:=[[27.7,99.7,-68.8,114.5,-106.3,-9.5],[9E9,9E9,9E9,9E9,9E9,9E9]];
PROC No_6_Tray()
	Set DO10_4;
	Reset DO10_5;
	MoveAbsJ JointTarget_5,v200,fine,tool0;
	MoveAbsJ JointTarget_1,v200,fine,tool0;
	MoveAbsJ JointTarget_7,v200,fine,tool0;
	MoveAbsJ JointTarget_2,v200,fine,tool0;
	waittime 0.5;
	Set DO10_5;
	Reset DO10_4;
	waittime 0.5;
	MoveAbsJ JointTarget_3,v200,fine,tool0;
	MoveAbsJ JointTarget_4,v200,fine,tool0;
	MoveAbsJ JointTarget_5,v200,fine,tool0;
	MoveAbsJ JointTarget_8,v200,fine,tool0;
	MoveAbsJ JointTarget_11,v200,fine,tool0;
	MoveAbsJ JointTarget_10,v200,fine,tool0;
	Set DO10_4;
	Reset DO10_5;
	waittime 0.5;
	MoveAbsJ JointTarget_9,v200,fine,tool0;
	MoveAbsJ JointTarget_8,v200,fine,tool0;
ENDPROC
PROC main()
	No_6_Tray;
ENDPROC


ENDMODULE