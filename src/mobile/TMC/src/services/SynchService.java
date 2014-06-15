package services;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import model.Constants;
import android.app.IntentService;
import android.content.Intent;
import android.os.Bundle;

public class SynchService extends IntentService
{
	private String urlString = Constants.SERVER_URL;
	private int command;

	public SynchService()
	{
		super("SynchService");
	}

	@Override
	protected void onHandleIntent(Intent intent)
	{
		Bundle parcel = intent.getBundleExtra("parcel");
		command = parcel.getInt("command", 0);

		switch (command)
		{

		case Constants.AUTHENTICATE_COMMAND:
			authenticate(parcel);
			break;

		case Constants.NEW_ORDER_COMMAND:
			placeNewOrder(parcel);
			break;

		case Constants.UPDATE_ORDERS_COMMAND:
			getIncompleteOrders();
			break;

		case Constants.DELETE_ORDER_COMMAND:
			deleteOrder(parcel);
			break;

		case Constants.MODIFY_ORDER_COMMAND:
			modifyOrder(parcel);
			break;

		case Constants.UPDATE_COMPLETED_ORDERS_COMMAND:
			getCompleteOrders(parcel);
			break;

		case Constants.EMERGENCY_STOP_COMMAND:
			emergencyStop();
			break;

		case Constants.START_COMMAND:
			machineStart();
			break;

		case Constants.STOP_COMMAND:
			machineStop();
			break;

		case Constants.GET_ALARM_COMMAND:
			getAlarms();
			break;

		default:
			break;

		}

	}

	private void getAlarms()
	{
		urlString += "GetAlarms";

		String response = connect(urlString);

		notifyCaller(response);

	}

	private void machineStop()
	{
		urlString += "StopScada";

		String response = connect(urlString);

		notifyCaller(response);
	}

	private void machineStart()
	{
		urlString += "StartScada";

		String response = connect(urlString);

		notifyCaller(response);
	}

	private void emergencyStop()
	{
		urlString += "EmergencyStopScada";

		String response = connect(urlString);

		notifyCaller(response);
	}

	private void modifyOrder(Bundle parcel)
	{
		urlString += "ModifyOrder/" + parcel.getString("orderName") + "/"
				+ parcel.getString("orderId") + "/"
				+ parcel.getString("status") + "/"

				+ parcel.getString("black") + "/" + parcel.getString("blue")
				+ "/" + parcel.getString("green") + "/"
				+ parcel.getString("red") + "/" + parcel.getString("white");

		String response = connect(urlString);

		notifyCaller(response);

	}

	private void deleteOrder(Bundle parcel)
	{
		urlString += "DeleteOrder/" + parcel.getString("orderId");

		String response = connect(urlString);

		notifyCaller(response);

	}

	private void getCompleteOrders(Bundle parcel)
	{
		urlString += "GetCompleteOrders/" + parcel.getString("from") + "/"
				+ parcel.getString("to");

		String response = connect(urlString);

		notifyCaller(response);
	}

	private void getIncompleteOrders()
	{
		urlString += "GetIncompleteOrders";

		String response = connect(urlString);

		notifyCaller(response);
	}

	private void placeNewOrder(Bundle parcel)
	{
		urlString += "PlaceOrder/" + parcel.getString("orderName") + "/"
				+ parcel.getString("black") + "/" + parcel.getString("blue")
				+ "/" + parcel.getString("green") + "/"
				+ parcel.getString("red") + "/" + parcel.getString("white");

		String response = connect(urlString);

		notifyCaller(response);
	}

	private void authenticate(Bundle parcel)
	{
		urlString += "Authenticate/" + parcel.getString("userName") + "/"
				+ parcel.getString("password");

		String response = connect(urlString);

		notifyCaller(response);
	}

	private String connect(String urlStr)
	{
		StringBuilder str = new StringBuilder();

		try
		{
			URL url = new URL(urlStr);
			HttpURLConnection con = (HttpURLConnection) url.openConnection();
			con.setRequestMethod("GET");
			con.setConnectTimeout(10000); // times out after 10 seconds

			con.setRequestProperty("Content-Type", "application/json");
			con.setRequestProperty("Accept", "application/json");
			BufferedReader in = new BufferedReader(new InputStreamReader(
					con.getInputStream()));

			String line;

			while ((line = in.readLine()) != null)
			{
				str.append(line);
			}
			in.close();
		}

		catch (Exception exc)
		{
			System.out.print("Exception: " + exc.toString());
		}

		return str.toString();

	}

	private void notifyCaller(String response)
	{
		Intent intent = new Intent();

		intent.setAction(Integer.toString(command));
		intent.putExtra("command", Integer.toString(command));
		intent.putExtra("result", response);
		sendBroadcast(intent);
	}

}
