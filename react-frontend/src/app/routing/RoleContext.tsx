import React, { createContext, useContext, useState } from 'react';
import { useAuth } from './AuthContext';

interface RoleContextType {
    roles: string[];
}

const RoleContext = createContext<RoleContextType>({
    roles: [],
});

export const useRoles = () => useContext(RoleContext);

interface RoleProviderProps {
    children: React.ReactNode;
}

const extractRolesFromToken = (token: string | null): string[] => {
    if (!token) return [];

    try {
        const payload = token.split('.')[1];
        const decodedPayload = atob(payload);
        const { role } = JSON.parse(decodedPayload);
        return role || [];
    } catch (error) {
        console.error('Failed to parse JWT token', error);
        return [];
    }
};

export const RoleProvider: React.FC<RoleProviderProps> = ({ children }) => {
    const [prevToken, setPrevToken] = useState<string | null>(() => {
        if (typeof window !== 'undefined') {
            const _token = localStorage.getItem('jwtToken');
            return _token;
        }
        return null;
    });

    const { token } = useAuth();
    const [roles, setRoles] = useState<string[]>(extractRolesFromToken(prevToken));

    if (prevToken != token) {
        setPrevToken(token);
        setRoles(extractRolesFromToken(token));
    }

    return (
        <RoleContext.Provider value={{ roles }}>
            {children}
        </RoleContext.Provider>
    );
};