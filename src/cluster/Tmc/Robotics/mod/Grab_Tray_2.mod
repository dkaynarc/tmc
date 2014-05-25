MODULE Module1
	CONST jointtarget JointTarget_1:=[[90.9,23.1,58.4,-1.0,-86.7,42.2],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_2:=[[89.6,24.7,49.6,-1.1,-79.5,42.4],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_3:=[[92.4,35.0,31.2,3.1,-71.3,40.8],[9E9,9E9,9E9,9E9,9E9,9E9]];

	CONST jointtarget JointTarget_4:=[[41.8,26.6,41.6,-47.8,-80.3,58.0],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_5:=[[92.4,31.6,32.2,3.2,-68.8,40.6],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_6:=[[-84.1,-53.8,60.9,20.7,-10.6,22.4],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_8:=[[17.7,78.1,-21.8,108.6,-92.7,11.7],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_7:=[[17.7,82.7,-25.3,108.6,-92.4,12.7],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_9:=[[28.6,85.4,-39.5,119.4,-100.9,-5.6],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_10:=[[26,64.1,-23.9,98.3,-104.1,-5.1],[9E9,9E9,9E9,9E9,9E9,9E9]];
PROC Path_10()
	SetDO DO10_4,1;
	Reset DO10_5;
	MoveAbsJ JointTarget_1,v200,fine,tool0;
	MoveAbsJ JointTarget_2,v200,fine,tool0;
	MoveAbsJ JointTarget_3,v200,fine,tool0;
	WaitTime 0.5;
	SetDO DO10_5,1;
	Reset DO10_4;
    WaitTime 0.5;
	MoveAbsJ JointTarget_5,v200,fine,tool0;
	MoveAbsJ JointTarget_4,v200,fine,tool0;
	MoveAbsJ JointTarget_6,v200,fine,tool0;
	MoveAbsJ JointTarget_8,v200,fine,tool0;
	MoveAbsJ JointTarget_7,v200,fine,tool0;
	SetDO DO10_4,1;
	Reset DO10_5;
	WaitTime 0.5;
	MoveAbsJ JointTarget_9,v200,fine,tool0;
	MoveAbsJ JointTarget_10,v200,fine,tool0;
ENDPROC
PROC main()
	Path_10;
ENDPROC

ENDMODULE