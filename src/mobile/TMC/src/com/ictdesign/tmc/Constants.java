/* COPYRIGHT (C) 2014 Carlo Chumroonridhi. All Rights Reserved. */

package com.ictdesign.tmc;

import java.util.ArrayList;
import java.util.Arrays;

/**
 * Defines all constants to be used globally between activities.
 * 
 * Array used for subject list is also predefined here.
 */

public class Constants
{
	public static final String PENDING = "PENDING";
	public static final String ACTIVE = "ACTIVE";
	public static final String COMPLETE = "COMPLETE";
	public static final String ON = "ON";
	public static final String OFF = "OFF";
	public static final String ROBOT = "ROBOT";
	public static final String CONVEYOR = "CONVEYOR";
	public static final ArrayList<Order> ORDERS = new ArrayList<Order>(
			Arrays.asList(new Order("Carlo's Order", "1029231", COMPLETE),
					new Order("Joel's Order", "2294714", ACTIVE), new Order(
							"Siarhei's Order", "3129214", PENDING), new Order(
							"Joel's Order", "2294714", PENDING), new Order(
							"Siarhei's Order", "3129214", PENDING), new Order(
							"Joel's Order", "2294714", PENDING), new Order(
							"Siarhei's Order", "3129214", PENDING), new Order(
							"Joel's Order", "2294714", PENDING), new Order(
							"Siarhei's Order", "3129214", PENDING), new Order(
							"Joel's Order", "2294714", PENDING), new Order(
							"Siarhei's Order", "3129214", PENDING), new Order(
							"Joel's Order", "2294714", PENDING), new Order(
							"Siarhei's Order", "3129214", PENDING), new Order(
							"Joel's Order", "2294714", PENDING), new Order(
							"Siarhei's Order", "3129214", PENDING), new Order(
							"Joel's Order", "2294714", PENDING), new Order(
							"Siarhei's Order", "3129214", PENDING), new Order(
							"Joel's Order", "2294714", PENDING), new Order(
							"Smits Order", "9365826", COMPLETE)));
	public static final ArrayList<Machine> MACHINES = new ArrayList<Machine>(
			Arrays.asList(new Machine("Sorter", ROBOT, ON), new Machine(
					"Assembler", ROBOT, OFF), new Machine("Loader", ROBOT, ON),
					new Machine("Palletiser", ROBOT, OFF), new Machine(
							"Conveyor #1", CONVEYOR, ON), new Machine(
							"Conveyor #2", CONVEYOR, OFF)));
	public static final String USERNAME = "mobile";
	public static final String PASSWORD = "ictd";
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
}