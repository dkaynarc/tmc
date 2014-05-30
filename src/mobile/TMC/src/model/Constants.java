/* COPYRIGHT (C) 2014 Carlo Chumroonridhi. All Rights Reserved. */

package model;

/**
 * Defines all constants to be used globally within the application.
 * 
 * Array used for order list is also predefined here.
 */

public class Constants
{
	public static final String PENDING = "Pending";
	public static final String ASSEMBLY = "Assembly";
	public static final String CANCELLED = "Cancelled";
	/* public static final String ACTIVE = "ACTIVE"; */
	public static final String COMPLETE = "Complete";
	public static final String ON = "ON";
	public static final String OFF = "OFF";
	public static final String ROBOT = "ROBOT";
	public static final String CONVEYOR = "CONVEYOR";
	/*
	 * public static final ArrayList<Order> ORDERS = new ArrayList<Order>(
	 * Arrays.asList(new Order(1029231, "Carlo", COMPLETE), ///new
	 * Order(2294714, "Joel", ACTIVE), new Order(3129214, "Siarhei",PENDING),
	 * new Order(2294714, "Joel", PENDING), new Order(3129214, "Siarhei",
	 * PENDING), new Order(2294714, "Joel",PENDING), new Order(3129214,
	 * "Siarhei", PENDING), new Order(2294714, "Joel", PENDING), new
	 * Order(3129214, "Siarhei", PENDING), new Order(2294714, "Joel",PENDING),
	 * new Order(3129214, "Siarhei", PENDING), new Order(2294714, "Joel",
	 * PENDING), new Order(3129214, "Siarhei", PENDING), new Order(2294714,
	 * "Joel",PENDING), new Order(3129214, "Siarhei", PENDING), new
	 * Order(9365826, "Smit", COMPLETE)));
	 */
	/*
	 * public static final ArrayList<Machine> MACHINES = new ArrayList<Machine>(
	 * Arrays.asList(new Machine("Sorter", ROBOT, ON), new Machine("Assembler",
	 * ROBOT, OFF), new Machine("Loader", ROBOT, ON), new Machine("Palletiser",
	 * ROBOT, OFF), new Machine("Conveyor #1", CONVEYOR, ON), new
	 * Machine("Conveyor #2", CONVEYOR, OFF)));
	 */
	/*
	 * public static final String USERNAME = "mobile"; public static final
	 * String PASSWORD = "ictd";
	 */
	public static final String WRONGINFO = "Incorrect username/password.";
	public static final String NAME = "Name";
	public static final String NUMBER = "Number";
	public static final String STATUS = "Status";
	public static final String COMPLETED_ORDERS = "COMPLETED ORDERS";
	public static final String ORDER_QUEUE = "ORDER QUEUE";
	public static final int REQUEST_CODE = 1;
	public static final String ENTER_NAME = "Enter order name!";
	public static final String ENTER_NUMBER = "Enter order number!";
	public static final int RESULT_OK = 1;
	public static final int RESULT_CANCEL = 0;
	public static final String MACHINE_STATUS = "MACHINE STATUS";
	public static final String CONTROLLER = "CONTROLLER";
	public static final String STOP_TITLE = "EMERGENCY STOP";
	public static final String STOP_CONFIRM = "Are you sure you want to \"Stop\"?";
	public static final String CANCEL = "Cancel";
	public static final String OK = "Ok";
	public static final String DELETE_TITLE = "Delete Order";
	public static final String DELETE_CONFIRM = "Are you sure you want to delete this Order?";
	public static final String LOGOUT = "LOGOUT";
	public static final String LOGOUT_CONFIRM = "Are you sure you want to logout?";
	public static final int STARTUP = 0;
	public static final int SHUTDOWN = 1;
	public static final String MODIFY = "Modify";
	public static final int CREATE_ORDER = 4;
	public static final int MODIFY_ORDER = 5;
	public static final String ID = "ID";
	public static final CharSequence NEW_ORDER_FAIL = "Placing new order failed";
	public static final int NEW_ORDER_COMMAND = 2;
	public static final String ENTER_ITEMS_QUANTITY = "Select some number of items";
	public static final String FEEDBACK = "FEEDBACK";
	public static final int AUTHENTICATE_COMMAND = 1;
	public static final String APP_PERSISTANCE = "USER_PERSISTANCE";
	public static final String USERNAME_KEY = "userName";
	public static final int UPDATE_ORDERS_COMMAND = 3;
	public static final String TOTAL_NUMBER_ERROR = "Total number of items can't be greater than 8";
	public static final int DELETE_ORDER_COMMAND = 4;
	public static final CharSequence DELETE_ORDER_FAIL = "DELETION OF THE ORDER FAILED";
	public static final int MODIFY_ORDER_COMMAND = 5;
	public static final CharSequence MODIFY_ORDER_FAIL = "Failed to modify selected order";
	public static final int UPDATE_COMPLETED_ORDERS_COMMAND = 6;
	public static final CharSequence NOT_AUTHORIZED = "You are not authorized to change this order";
	public static final int MACHINE_STATUS_COMMAND = 7;
	public static final CharSequence ATTENTION = "Attention!";
	public static final CharSequence MACHINE_STOP = "Some machinery stopped unexpectedly";
	public static final long UPDATE_INTERVAL = 15000;// milliseconds
	public static final int EMERGENCY_STOP_COMMAND = 8;
	public static final CharSequence STOP_SUCCESS = "The system has been stopped";
	public static final CharSequence STOP_FAIL = "Failed to stop the system";
	public static final CharSequence START_SUCCESS = "The system has been started";
	public static final CharSequence START_FAIL = "Failed to start the system";
	public static final int START_COMMAND = 9;
	public static final int STOP_COMMAND = 10;
	//public static final String SERVER_URL = "http://stesha.com.au/api/Server/";
	// public static final String SERVER_URL =
	// "http://172.19.14.237:9000/api/Server/";
	public static final String SERVER_URL = "http://192.168.1.3:9000/api/Server/";
	public static final String ENVIRONMENT = "ENVIRONMENT";
	public static final String BLACK = "Black";
	public static final String BLUE = "Blue";
	public static final String GREEN = "Green";
	public static final String RED = "Red";
	public static final String WHITE = "White";
	public static final String START_TIME = "Start Time";
	public static final String FINISH_TIME = "Finish Time";
	public static final int ENV_UPDATE_COMMAND = 11;
	public static final CharSequence ENV_UPDATE_FAIL = "Environment data update failed";
	public static final String ALARMS = "ALARMS";
	public static final String DATE_FORMAT = "dd/MM/yyyy";
	public static final int GET_ALARM_COMMAND = 12;
}
