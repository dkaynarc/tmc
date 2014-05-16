/* COPYRIGHT (C) 2014 Carlo Chumroonridhi. All Rights Reserved. */

package fragments;

import ictd.activities.R;

import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

/**
 * Implements the logout fragment. Opens a dialog to confirm exiting of the
 * module activity and redirects back to the login activity.
 */

public class EnvironmentFragment extends Fragment
{
	
	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState)
	{
		View rootView = inflater.inflate(R.layout.fragment_environment, container,
				false);
		return rootView;
	}
}
