// contexts/AuthContext.tsx
import axios from 'axios';
import React, { createContext, useContext, useState } from 'react';

interface AuthContextType {
  token: string | null;
  setToken: (token: string | null) => void;
}

const AuthContext = createContext<AuthContextType>({
  token: null,
  setToken: () => {},
});

export const useAuth = () => useContext(AuthContext);

interface AuthProviderProps {
  children: React.ReactNode;
}

export const AuthProvider: React.FC<AuthProviderProps> = ({ children }) => {
  const [token, _setToken] = useState<string | null>(() => {
    if (typeof window !== 'undefined') {
      const _token = localStorage.getItem('jwtToken');
      axios.defaults.headers.common["Authorization"] = "Bearer " + _token;
      console.log("Loaded token: " + _token);
      return _token;
    }
    return null;
  });

  const setToken = (newToken: string | null) => {
    if (typeof window !== 'undefined') {
      if (newToken) {
        axios.defaults.headers.common["Authorization"] = "Bearer " + newToken;
        localStorage.setItem('jwtToken', newToken);
      } else {
        localStorage.removeItem('jwtToken');
      }
    }
    _setToken(newToken);
  };

  return (
    <AuthContext.Provider value={{ token, setToken }}>
      {children}
    </AuthContext.Provider>
  );
};