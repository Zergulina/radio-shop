import { useRoles } from "./RoleContext"

export const useRoleBasedView = <T,>(views: Record<string, T>, defaultView: T): T => {
  const { roles } = useRoles();
  
  for (const [role, view] of Object.entries(views)) {
    if (roles.includes(role)) {
      return view;
    }
  }
  
  return defaultView;
};