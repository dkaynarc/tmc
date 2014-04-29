/* COPYRIGHT (C) 2014 Carlo Chumroonridhi. All Rights Reserved. */

package fragments;

import model.Constants;
import model.Machine;

import com.ictdesign.tmc.R;

import adapters.MachineStatusAdapter;
import android.annotation.SuppressLint;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.graphics.Color;
import android.graphics.ColorFilter;
import android.graphics.LightingColorFilter;
import android.graphics.drawable.Drawable;
import android.media.MediaPlayer;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.support.v4.app.ListFragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.ListView;

/**
 * Sets the layout for the activity.
 * 
 * Creates a new adapter for a list of machines using predefined values.
 * 
 * Also implements the shutdown, start-up and emergency stop buttons.
 */

public class MachineStatusFragment extends ListFragment
{
	MediaPlayer mMediaPlayer = new MediaPlayer();

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
	 * Implements the handler for the new threads created by the startup and
	 * shutdown buttons. Only necessary for demonstration purposes.
	 */
	
	@SuppressLint("HandlerLeak")
	private Handler mHandler = new Handler() {
		@Override
		public void handleMessage(Message msg)
		{
			switch (msg.what)
			{
			case Constants.SHUTDOWN:
				turnOff();
				break;
			case Constants.STARTUP:
				turnOn();
				break;
			}
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
				R.layout.machine_row, Constants.MACHINES));
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
		new Thread() {
			@Override
			public void run()
			{
				playSound(R.raw.startup);
				// Replace while and mHandler method with startup() function
				while (mMediaPlayer.isPlaying())
				{
				}
				mHandler.sendEmptyMessage(Constants.STARTUP);
			}
		}.start();
	}

	/**
	 * Creates a new thread, plays the shutdown sound and then turns machines
	 * off when sound finishes playing. Only used for demonstrative purposes,
	 * will need some further implementation.
	 */
	
	public void shutdown()
	{
		new Thread() {
			@Override
			public void run()
			{
				playSound(R.raw.shutdown);
				// Replace while and mHandler method with shutdown() function
				while (mMediaPlayer.isPlaying())
				{
				}
				mHandler.sendEmptyMessage(Constants.SHUTDOWN);
			}
		}.start();
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
		// Replace turnOff() with emergencyStop() method
		turnOff();
		playSound(R.raw.stop);
	}

	/**
	 * Plays the sound of the id given.
	 * 
	 * @param soundId
	 */
	
	public void playSound(int soundId)
	{
		if (mMediaPlayer.isPlaying())
			mMediaPlayer.stop();
		mMediaPlayer = MediaPlayer.create(getActivity(), soundId);
		mMediaPlayer.setLooping(false);
		mMediaPlayer.start();
	}
}
