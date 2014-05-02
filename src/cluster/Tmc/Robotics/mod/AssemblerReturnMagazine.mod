MODULE MainModule
    CONST jointtarget hoverlow:=[[-95,35.4,16.4,-5.8,-55.5, 4.1],[9E9,9E9,9E9,9E9,9E9,9E9]];
    CONST jointtarget touching:=[[-95, 40.5, 16.6, -5.4, -61.1, 3.4],[9E9,9E9,9E9,9E9,9E9,9E9]];
    CONST jointtarget hoverhigh:=[[-95,21.9, 7.4, -8.7, -33.2, 8.1],[9E9,9E9,9E9,9E9,9E9,9E9]];
    CONST jointtarget midpoint:=[[-24.4, -13.1, 36.2, -2.37, -36, 22],[9E9,9E9,9E9,9E9,9E9,9E9]];
    
    CONST jointtarget hoverconveyor:=[[41.5, 49.1, -16.5, -62.7, -59.9, 55.4],[9E9,9E9,9E9,9E9,9E9,9E9]];
    CONST jointtarget touchingconveyor:=[[34.2, 43.6, 1.3, -63.4, -70.6, 46.2],[9E9,9E9,9E9,9E9,9E9,9E9]];
	CONST jointtarget touchingconveyor2:=[[42.4, 54.6, -17.8, -59.9, -61.2, 50.8],[9E9,9E9,9E9,9E9,9E9,9E9]];
    
    PROC main()
        MoveAbsJ hoverhigh, v200, fine, tool0;
        MoveAbsJ touching, v200, fine, tool0;
        Set DO10_6;
        MoveAbsJ hoverhigh, v200, fine, tool0;
        MoveAbsJ midpoint, v200, fine, tool0;
        MoveAbsJ hoverconveyor, v200, fine, tool0;
		MoveAbsJ touchingconveyor2, v200, fine, tool0;
        MoveAbsJ touchingconveyor, v200, fine, tool0;
        Reset DO10_6;
		
    ENDPROC
ENDMODULE
