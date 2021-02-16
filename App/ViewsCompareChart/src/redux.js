import { combineReducers, createStore } from 'redux';

// actions.js
export const getData = () => ({
  type: 'GET_DATA',
});

export const setData = data => ({
  type: 'SET_DATA',
  data
});

export const deleteData = () => ({
  type: 'DELETE_DATA',
});

// reducers.js
export const data = (state = {}, action) => {
  switch (action.type) {
    case 'GET_DATA':
      return state.data;
    case 'SET_DATA':
      return action.data;
    case 'DELETE_DATA':
        return {};
    default:
      return state;
  }
};

export const reducers = combineReducers({
  data,
});

// store.js
export function configureStore(initialState = { }) 
{
  const store = createStore(reducers, initialState);
  
  return store;
}

export const store = configureStore();