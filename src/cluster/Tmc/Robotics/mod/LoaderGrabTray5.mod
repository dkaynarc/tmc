MODULE Module1
	CONST jointtarget JointTarget_1:=[[93.5,-60.7,49.4,-11.5,8.3,53.4],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_2:=[[94.8,-29.9,45.8,12.9,-29.5,32.4],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_3:=[[93.4,-14.7,29.2,12.4,22.1,35],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_4:=[[93.5,4.0,9.8,14.1,-21.2,31.8],[9E9,9E9,9E9,9E9,9E9,9E9]];
	
	CONST jointtarget JointTarget_6:=[[108.5,-62.6,48.2,108.3,-21.5,-65.2],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_8:=[[17.7,78.1,-21.8,108.6,-92.7,11.7],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_7:=[[17.7,82.7,-25.3,108.6,-92.4,12.7],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_9:=[[21.9,97.4,-61.9,113.8,-104.6,-4.2],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_10:=[[26,64.1,-23.9,98.3,-104.1,-5.1],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_11:=[[105,-42.4,46.8,57.2,-26.3,-14.8],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget JointTarget_12:=[[93.3,-13.3,30.6,11.2,-26.9,35.4],[9E9,9E9,9E9,9E9,9E9,9E9]];
PROC Path_10()
	SetDO DO10_4,1;
	Reset DO10_5;
	MoveAbsJ JointTarget_1,v200,fine,tool0;
	MoveAbsJ JointTarget_2,v200,fine,tool0;
	MoveAbsJ JointTarget_12,v200,fine,tool0;
	MoveAbsJ JointTarget_4,v200,fine,tool0;
	WaitTime 0.5;
	SetDO DO10_5,1;
	Reset DO10_4;
    WaitTime 0.5;
	MoveAbsJ JointTarget_11,v200,fine,tool0;
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