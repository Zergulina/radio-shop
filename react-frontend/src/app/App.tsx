import { AuthProvider } from "./routing/AuthContext"
import { RoleProvider } from "./routing/RoleContext"
import Routes from "./routing/Routes"

function App() {

  return (
    <AuthProvider>
      <RoleProvider>
        <Routes/>
      </RoleProvider>
    </AuthProvider>
  )
}

export default App
