MODULE MainModule
    CONST jointtarget slot1hover:= [[-90, 51.7, -9.1, 0.4, -46.2, 0.2],[9E9,9E9,9E9,9E9,9E9,9E9]];
    CONST jointtarget slot1contact:= [[-90, 55.1, -9.1, 0.4, -49.6, 0.2],[9E9,9E9,9E9,9E9,9E9,9E9]];
    PROC main()
        MoveAbsJ slot1hover, v300, fine, tool0;
        MoveAbsJ slot1contact, v300, fine, tool0;
        Set DO10_6;
        MoveAbsJ slot1hover, v300, fine, tool0;
        Reset DO10_6;
    ENDPROC
ENDMODULE