import { CartProvider } from "../store/CartContext"
import { AuthProvider } from "./routing/AuthContext"
import { RoleProvider } from "./routing/RoleContext"
import Routes from "./routing/Routes"

function App() {

  return (
    <AuthProvider>
      <RoleProvider>
        <CartProvider>
          <Routes/>
        </CartProvider>
      </RoleProvider>
    </AuthProvider>
  )
}

export default App
