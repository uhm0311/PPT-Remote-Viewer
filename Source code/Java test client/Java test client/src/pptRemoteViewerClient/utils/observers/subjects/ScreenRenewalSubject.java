package pptRemoteViewerClient.utils.observers.subjects;

import java.util.ArrayList;

import pptRemoteViewerClient.utils.observers.ScreenRenewalObserver;

public abstract class ScreenRenewalSubject {
	protected ArrayList<ScreenRenewalObserver> observers = new ArrayList<ScreenRenewalObserver>();
	
	public void addObserver(ScreenRenewalObserver observer) {
		observers.add(observer);
	}
	
	public void removeObserver(ScreenRenewalObserver observer) {
		observers.remove(observer);
	}
	
	public abstract void notify(Object parameter);
}
