locals {
  tags = {
    application = "OrderFlow"
    environment = "dev"
    managed_by  = "Terraform"
  }
}

resource "azurerm_resource_group" "orderflow" {
  name     = var.resource_group_name
  location = var.location

  tags = local.tags
}

resource "azurerm_container_registry" "orderflow" {
  name                = var.acr_name
  resource_group_name = azurerm_resource_group.orderflow.name
  location            = azurerm_resource_group.orderflow.location

  sku           = "Basic"
  admin_enabled = false

  tags = local.tags
}