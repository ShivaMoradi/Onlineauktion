
import { BrowserRouter, Routes, Route } from "react-router-dom"
import MyNav from "./MyNav.jsx"
import HomePage from "../pages/HomePage.jsx"
import MySearchInputPage from "../pages/MySearchInputPage.jsx"
import ContactPage from "../pages/ContactPage.jsx"
import { GlobalProvider } from './GlobalContext.jsx'
import Cart from '../pages/Cart.jsx'
import CarDetail from "../pages/CarDetails.jsx"
import ShowAuctionPage from "../pages/ShowAuctionPage.jsx"
<<<<<<< HEAD
import RegisterUser from '../pages/RegisterUser.jsx'
=======
import UserPage from "../pages/UserPage.jsx"

>>>>>>> feature/userpage

function MyRouter() {

  return (
    <GlobalProvider>
      <BrowserRouter>
        <div className="page corners padding transp">
          <MyNav />

          <main>
            <div>
              <Routes>
                <Route path="/" element={<HomePage />}></Route>
                <Route path="/my-search-input-page" element={<MySearchInputPage />} />
                <Route path="/contact-page" element={<ContactPage />} />
                <Route path="/cars/:id" element={< CarDetail />} />
<<<<<<< HEAD
                <Route path="/cart-page" element={ <Cart /> } />
                <Route path="/registering-page" element={ <RegisterUser /> } />
                <Route path="/show-auction-page" element={<ShowAuctionPage/>} />
=======
                <Route path="/cart-page" element={<Cart />} />
                <Route path="/show-auction-page" element={<ShowAuctionPage />} />
                <Route path="/userpage" element={<UserPage />} />
>>>>>>> feature/userpage
              </Routes>
            </div>
          </main>
        </div>
      </BrowserRouter>
    </GlobalProvider>
  )

}

export default MyRouter
