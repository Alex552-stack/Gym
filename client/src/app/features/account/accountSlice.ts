import { createAsyncThunk, createSlice, isAnyOf } from "@reduxjs/toolkit"; // External library
import { FieldValues } from "react-hook-form"; // External library
import { toast } from "react-toastify"; // External library
import { User } from "../../../models/user"; // Your own module
import agent from "../../api/agent"; // Your own module
import { router } from "../../Router/Routes"; // Your own module


interface AccountState{
    user: User | null;
}

const initialState: AccountState = {
    user : null
}

export const signInUser = createAsyncThunk<User, FieldValues>(
    'account/signInUser',
    async (data, thunkAPI) => {
        try{
            const userDto = await agent.Account.login(data);
            const {...user} = userDto;
            localStorage.setItem('user', JSON.stringify(user));
            return user;
        } catch (error : any) {
            return thunkAPI.rejectWithValue({error: error.data})
        }
    }
)

export const fetchCurrentUser = createAsyncThunk<User>(
    'account/fetchCurrentUser',
    async (_, thunkAPI) => {
        thunkAPI.dispatch(setUser(JSON.parse(localStorage.getItem('user')!)))
        try {
            const userDto = await agent.Account.currentUser();
            const {...user} = userDto;
            localStorage.setItem('user', JSON.stringify(user));
            if(!user.emailConfirmed) toast.warning("Your email is not confirmed");
            return user;
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.data })
        }
    },
    {
        condition: () => {
            if(!localStorage.getItem('user')) return false;
        }
    }
)

export const accountSlice = createSlice({
    name: 'account',
    initialState,
    reducers: {
        signOut: (state) => {
            state.user = null;
            localStorage.removeItem('user');
            //router.navigate('/);
        },
        setUser: (state,action) => {
            let claims = JSON.parse(atob(action.payload.token.split('.')[1]));
            let roles = claims['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
            state.user = {...action.payload, roles: typeof(roles) === 'string' ? [roles] : roles};
        }
    },
    extraReducers: (builder => {
        builder.addCase(fetchCurrentUser.rejected, (state) => {
            state.user = null;
            localStorage.removeItem('user');
            toast.error('Sesiune expirata - te rog reconecteaza-te');
            router.navigate('/');
        })
        builder.addMatcher(isAnyOf(signInUser.fulfilled, fetchCurrentUser.fulfilled), (state, action) => {
            let claims = JSON.parse(atob(action.payload.token.split(".")[1]));
            let roles = claims['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
            state.user = {...action.payload, roles : typeof(roles) === "string" ? [roles] : roles};
        });
        builder.addMatcher(isAnyOf(signInUser.rejected,), (_state, action) => {
            console.log(action.payload);
        })
    })
})

export const {signOut, setUser} = accountSlice.actions;