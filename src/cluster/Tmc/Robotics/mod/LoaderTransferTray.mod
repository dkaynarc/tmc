MODULE Module1
	
	
	CONST jointtarget JointTarget_10:=[[26,64.1,-23.9,98.3,-104.1,-5.1],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_1:=[[21.8,80.6,-31.6,117.7,-89.5,4.1],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_2:=[[9.7,74.4,-18.5,107.8,-80.1,7.6],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_3:=[[-135.2,22.7,37.3,45.4,-72.6,25.3],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_4:=[[-135.2,73.6,18.8,43.1,-93.3,48.0],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_5:=[[-119.2,72.5,30.9,60.0,-98.3,57.8],[9E9,9E9,9E9,9E9,9E9,9E9]];
  	CONST jointtarget JointTarget_6:=[[7.3,68.0,-13.8,106.1,-79.3,5.4],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_7:=[[8.1,70.9,-15.7,106.6,-79.4,6.6],[9E9,9E9,9E9,9E9,9E9,9E9]];
PROC Path_10()
	SetDO DO10_4,1;
	Reset DO10_5;
	MoveAbsJ JointTarget_10,v200,fine,tool0;
	MoveAbsJ JointTarget_1,v200,fine,tool0;
	MoveAbsJ JointTarget_2,v50,fine,tool0;
	MoveAbsJ JointTarget_7,v10,fine,tool0;
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