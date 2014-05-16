MODULE Module1
	CONST jointtarget JointTarget_1:=[[91.1,-28.7,58.7,-1.4,-35.4,43.3],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_2:=[[90.8,-12.9,44.6,-1.9,-37.0,43.7],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_3:=[[91.0,7.6,21.4,2.0,-31.9,41.3],[9E9,9E9,9E9,9E9,9E9,9E9]];

	CONST jointtarget JointTarget_4:=[[93.7,-53.9,60.9,20.7,-10.6,22.4],[9E9,9E9,9E9,9E9,9E9,9E9]];
	
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