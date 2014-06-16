/* COPYRIGHT (C) 2014 Carlo Chumroonridhi. All Rights Reserved. */

package fragments;

import java.util.ArrayList;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import model.Constants;
import model.Machine;
import ictd.activities.R;
import adapters.MachineStatusAdapter;
import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.IntentFilter;
import android.graphics.Color;
import android.graphics.ColorFilter;
import android.graphics.LightingColorFilter;
import android.graphics.drawable.Drawable;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v4.app.ListFragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.Toast;

/**
 * Sets the layout for the activity.
 * 
 * Creates a new adapter for a list of machines using predefined values.
 * 
 * Also implements the shutdown, start-up and emergency stop buttons.
 */

public class MachineStatusFragment extends ListFragment
{
	private ResultReceiver receiver;
	private ProgressDialog pd;
	private Timer timer;

	private OnClickListener onEmergencyStopClickListener = new OnClickListener() {
		public void onClick(View view)
		{
			Drawable myIcon = getResources().getDrawable(
					android.R.drawable.ic_dialog_alert);
			ColorFilter filter = new LightingColorFilter(Color.RED, Color.RED);
			myIcon.setColorFilter(filter);
			new AlertDialog.Builder(getActivity())
					.setIcon(myIcon)
					.setTitle(Constants.STOP_TITLE)
					.setMessage(Constants.STOP_CONFIRM)
					.setPositiveButton(Constants.OK,
							new DialogInterface.OnClickListener() {
								public void onClick(DialogInterface dialog,
										int id)
								{
									emergencyStop();
									dialog.cancel();
								}
							})
					.setNegativeButton(Constants.CANCEL,
							new DialogInterface.OnClickListener() {
								public void onClick(DialogInterface dialog,
										int id)
								{
									dialog.cancel();
								}
							}).show();
		}
	};

	/**
	 * Implements the startup onClickListener
	 */

	private OnClickListener onStartupClickListener = new OnClickListener() {
		public void onClick(final View view)
		{
			startup();
		}
	};

	/**
	 * Implements the shutdown onClickListener
	 */

	private OnClickListener onShutdownClickListener = new OnClickListener() {
		public void onClick(final View view)
		{
			shutdown();
		}
	};

	/**
	 * Sets the layout for the activity.
	 * 
	 * Creates a new adapter for a list of machines using predefined values and
	 * sets the onClickListeners for the buttons.
	 */

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState)
	{
		View rootView = inflater.inflate(R.layout.list_machines, container,
				false);
		((Button) rootView.findViewById(R.id.machinelist_startup_b))
				.setOnClickListener(onStartupClickListener);
		((Button) rootView.findViewById(R.id.machinelist_shutdown_b))
				.setOnClickListener(onShutdownClickListener);
		((Button) rootView.findViewById(R.id.machinelist_emergencystop_b))
				.setOnClickListener(onEmergencyStopClickListener);

		setListAdapter(new MachineStatusAdapter(getActivity(),
				R.layout.machine_row, new ArrayList<Machine>()/* dummy array */));
		return rootView;
	}

	/**
	 * Implements each machine's "onClickListener".
	 * 
	 * Basically just displays a dialog with its status.
	 */

	@Override
	public void onListItemClick(ListView l, View v, int position, long id)
	{
		Machine machine = (Machine) getListAdapter().getItem(position);
		new AlertDialog.Builder(getActivity())
				.setIcon(
						((ImageView) v
								.findViewById(R.id.machinerow_machinetype_iv))
								.getDrawable())
				.setTitle(machine.getMachineName())
				.setMessage(
						String.format("%s: %s", Constants.STATUS,
								machine.getMachineStatus()))
				.setPositiveButton(Constants.OK,
						new DialogInterface.OnClickListener() {
							public void onClick(DialogInterface dialog, int id)
							{
								dialog.cancel();
							}
						}).show();
	}

	/**
	 * Creates a new thread, plays the startup sound and then turns machines on
	 * when sound finishes playing. Only used for demonstrative purposes, will
	 * need some further implementation.
	 */

	public void startup()
	{
		pd = ProgressDialog.show(getActivity(), null, "Contacting server");
		makeMachineService(Constants.START_COMMAND);
	}

	/**
	 * Creates a new thread, plays the shutdown sound and then turns machines
	 * off when sound finishes playing. Only used for demonstrative purposes,
	 * will need some further implementation.
	 */

	public void shutdown()
	{
		pd = ProgressDialog.show(getActivity(), null, "Contacting server");
		makeMachineService(Constants.STOP_COMMAND);
	}

	/**
	 * Turns all machines in the adapter on. Only used for demonstrative
	 * purposes.
	 */

	public void turnOn()
	{
		MachineStatusAdapter adapter = (MachineStatusAdapter) getListView()
				.getAdapter();
		Machine machine;
		for (int i = 0; i < adapter.getCount(); i++)
		{
			machine = adapter.getItem(i);
			machine.setMachineStatus(Constants.ON);
		}
		adapter.notifyDataSetChanged();
	}

	/**
	 * Turns all machines in the adapter off. Only used for demonstrative
	 * purposes.
	 */

	public void turnOff()
	{
		MachineStatusAdapter adapter = (MachineStatusAdapter) getListView()
				.getAdapter();
		Machine machine;
		for (int i = 0; i < adapter.getCount(); i++)
		{
			machine = adapter.getItem(i);
			machine.setMachineStatus(Constants.OFF);
			adapter.notifyDataSetChanged();
		}
	}

	/**
	 * Instantly turns all machines in the adapter off and then plays the
	 * respective sound. Only used for demonstrative purposes.
	 */

	public void emergencyStop()
	{
		// /////////////
		pd = ProgressDialog.show(getActivity(), null, "Contacting server");
		makeMachineService(Constants.EMERGENCY_STOP_COMMAND);
		// /////////////
	}

	// /////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// private class
	private class ResultReceiver extends BroadcastReceiver
	{
		@Override
		public void onReceive(Context context, Intent intent)
		{
			if (pd != null)
				pd.dismiss();

			String response = intent.getStringExtra("result");
			switch (Integer.decode(intent.getStringExtra("command")))
			{
			case Constants.MACHINE_STATUS_COMMAND:
				handleMachineStatus(response);
				break;

			case Constants.EMERGENCY_STOP_COMMAND:
				handleEmergencyStop(response);
				break;

			case Constants.START_COMMAND:
				handleStart(response);
				break;

			case Constants.STOP_COMMAND:
				handleStop(response);
				break;

			}
		}
	}

	private void handleStop(String response)
	{
		if (response.equalsIgnoreCase("\"success\""))
		{
			Toast.makeText(getActivity(), Constants.STOP_SUCCESS,
					Toast.LENGTH_SHORT).show();
			turnOff();
		}

		else
			Toast.makeText(getActivity(), Constants.STOP_FAIL,
					Toast.LENGTH_SHORT).show();
	}

	private void handleStart(String response)
	{
		if (response.equalsIgnoreCase("\"success\""))
		{
			Toast.makeText(getActivity(), Constants.START_SUCCESS,
					Toast.LENGTH_SHORT).show();
			turnOn();
		}

		else
			Toast.makeText(getActivity(), Constants.START_FAIL,
					Toast.LENGTH_SHORT).show();
	}

	private void handleEmergencyStop(String response)
	{
		if (response.equalsIgnoreCase("\"success\""))
		{
			Toast.makeText(getActivity(), Constants.STOP_SUCCESS,
					Toast.LENGTH_SHORT).show();
			turnOff();
		}

		else
			Toast.makeText(getActivity(), Constants.STOP_FAIL,
					Toast.LENGTH_SHORT).show();
	}

	private void handleMachineStatus(String response)
	{
		ArrayList<Machine> newStats = new ArrayList<Machine>();
		try
		{
			JSONArray arr = new JSONArray(response);

			for (int i = 0; i < arr.length(); i++)
			{
				JSONObject jObj = arr.getJSONObject(i);

				newStats.add(new Machine(jObj.getString("Name"), jObj
						.getString("Status")));
			}
		}

		catch (JSONException e)
		{
			Toast.makeText(getActivity(), Constants.UPDATE_FAIL,
					Toast.LENGTH_SHORT).show();
			return;
		}

		// check whether status of any machine changed

		MachineStatusAdapter adapter = (MachineStatusAdapter) getListView()
				.getAdapter();

		if (adapter.getCount() != 0 && newStats.size() != 0)
		{
			doStatusCheck(adapter, newStats);
			Toast.makeText(getActivity(), "Updated machine status",
					Toast.LENGTH_SHORT).show();// to be removed
		}

		adapter.clear();
		adapter.addAll(newStats);
		adapter.notifyDataSetChanged();
	}

	private void doStatusCheck(MachineStatusAdapter adapter,
			ArrayList<Machine> newStats)
	{
		ArrayList<Machine> changedMach = new ArrayList<Machine>();

		for (int i = 0; i < adapter.getCount(); i++)
		{
			if (!newStats
					.get(i)
					.getMachineStatus()
					.equalsIgnoreCase(
							((Machine) adapter.getItem(i)).getMachineStatus()))
			{
				changedMach.add(newStats.get(i));
			}

		}

		if (changedMach.size() > 0)
		{
			showAlertDialog(buildMessage(changedMach), changedMach.get(0)
					.getMachineStatus());
		}
	}

	private String buildMessage(ArrayList<Machine> changedMach)
	{
		StringBuilder message = new StringBuilder();
		for (Machine mach : changedMach)
		{
			message.append("\n\r" + mach.getMachineName());
		}

		return message.toString();
	}

	@Override
	public void onStart()
	{
		super.onStart();
		receiver = new ResultReceiver();
		getActivity().registerReceiver( receiver, new IntentFilter(Integer.toString(Constants.MACHINE_STATUS_COMMAND)));
		getActivity().registerReceiver(receiver, new IntentFilter(Integer.toString(Constants.STOP_COMMAND)));
		getActivity().registerReceiver(receiver, new IntentFilter(Integer.toString(Constants.START_COMMAND)));
		getActivity().registerReceiver(receiver, new IntentFilter(Integer.toString(Constants.EMERGENCY_STOP_COMMAND)));

		// initial fill of the view with machine status info
		makeMachineService(Constants.MACHINE_STATUS_COMMAND);
		startTimer(Constants.UPDATE_INTERVAL);

	}

	public void makeMachineService(int command)
	{
		Intent service;

		if (command == Constants.MACHINE_STATUS_COMMAND)
			service = new Intent(getActivity(),
					services.MachineUpdateService.class);

		else
			service = new Intent(getActivity(), services.SynchService.class);

		Bundle parcel = new Bundle();
		parcel.putInt("command", command);
		service.putExtra("parcel", parcel);

		// getActivity().stopService(service);
		getActivity().startService(service);
	}

	@Override
	public void onStop()
	{
		getActivity().unregisterReceiver(receiver);
		stopTimer();
		if(pd != null)
			pd.dismiss();
		
		super.onStop();
	}

	private void showAlertDialog(String message, String status)
	{
		Drawable icon = getResources().getDrawable(
				android.R.drawable.ic_dialog_alert);

		if (status.equalsIgnoreCase(Constants.ON))
		{
			icon.setColorFilter(new LightingColorFilter(Color.GREEN,
					Color.GREEN));
		}
		else
			icon.setColorFilter(new LightingColorFilter(Color.RED, Color.RED));

		new AlertDialog.Builder(getActivity())
				.setIcon(icon)
				.setTitle(Constants.ATTENTION)
				.setMessage(
						"The status of the following machinery changed: "
								+ message)
				.setNegativeButton(Constants.OK,
						new DialogInterface.OnClickListener() {
							public void onClick(DialogInterface dialog, int id)
							{
								dialog.cancel();
							}
						}).show();
	}

	private void stopTimer()
	{
		timer.cancel(true);
	}

	private void startTimer(long i)
	{
		timer = new Timer();
		timer.execute(i);
	}

	private class Timer extends AsyncTask<Long, Object, Object>
	{

		@Override
		protected String doInBackground(Long... params)
		{
			try
			{
				Thread.sleep(params[0]);
				return "done";
			}

			catch (InterruptedException e)
			{
				e.printStackTrace();
				return "done";
			}
		}

		@Override
		protected void onPostExecute(Object result)
		{
			// this creates regular updates of the view
			makeMachineService(Constants.MACHINE_STATUS_COMMAND);
			startTimer(Constants.UPDATE_INTERVAL);
		}

	}

}
