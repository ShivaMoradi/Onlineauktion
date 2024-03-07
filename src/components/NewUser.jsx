
async function NewUser ( event ) {
 
    event.preventDefault()
    const data = new FormData( event.target )
  const info = Object.fromEntries( data )
  try {
    const response = await fetch( "/api/users", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify( info ),

    } )
    if (response.ok) {
      alert("Successfully registerd!")
    } else {
      alert("Failed! Please try again.")
    }
    
      
  } catch ( error ) {
    console.error( "Error during registration:", error )
    alert( "An error occurred. Please try again later." )
  }
  
  }  

/*const Success = () => {
  alert("sucess")
}*/



export default NewUser
