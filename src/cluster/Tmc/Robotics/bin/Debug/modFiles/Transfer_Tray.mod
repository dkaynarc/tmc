MODULE Module1
	
	
	CONST jointtarget JointTarget_10:=[[26,64.1,-23.9,98.3,-104.1,-5.1],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_1:=[[22.6,74.9,-22.1,117.9,-86,8.1],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_2:=[[3,68.4,-8,100.9,-73.4,11.5],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_3:=[[-135.2,22.7,37.3,45.4,-72.6,25.3],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_4:=[[-135.2,73.6,18.8,43.1,-93.3,48.0],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_5:=[[-119.2,72.5,30.9,60.0,-98.3,57.8],[9E9,9E9,9E9,9E9,9E9,9E9]];
    CONST jointtarget JointTarget_6:=[[3.3,70.7,-10.9,101.9,-81.8,13],[9E9,9E9,9E9,9E9,9E9,9E9]];
PROC Path_10()
	SetDO DO10_4,1;
	Reset DO10_5;
	MoveAbsJ JointTarget_10,v200,fine,tool0;
	MoveAbsJ JointTarget_1,v200,fine,tool0;
	MoveAbsJ JointTarget_2,v100,fine,tool0;
  	MoveAbsJ JointTarget_6,v10,fine,tool0;
	WaitTime 1;
	SetDO DO10_5,1;
	Reset DO10_4;
    WaitTime 0.5;
	MoveAbsJ JointTarget_10,v200,fine,tool0;
	MoveAbsJ JointTarget_3,v200,fine,tool0;
	MoveAbsJ JointTarget_4,v200,fine,tool0;
	WaitTime 0.5;
	SetDO DO10_4,1;
	Reset DO10_5;
	WaitTime 0.5;
	MoveAbsJ JointTarget_5,v200,fine,tool0;
	MoveAbsJ JointTarget_10,v200,fine,tool0;
ENDPROC
PROC main()
	Path_10;
ENDPROC

ENDMODULE